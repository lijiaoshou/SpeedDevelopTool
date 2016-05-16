
using CSharpEditor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace CodeRegion
{
    public partial class MainForm : Form
    {
        internal ICSharpCode.SharpDevelop.Dom.ProjectContentRegistry pcRegistry;
        internal ICSharpCode.SharpDevelop.Dom.DefaultProjectContent myProjectContent;
        internal ICSharpCode.SharpDevelop.Dom.ParseInformation parseInformation = new ICSharpCode.SharpDevelop.Dom.ParseInformation();
        ICSharpCode.SharpDevelop.Dom.ICompilationUnit lastCompilationUnit;
        Thread parserThread;

        public static bool IsVisualBasic = false;

        /// <summary>
        /// Many SharpDevelop.Dom methods take a file name, which is really just a unique identifier
        /// for a file - Dom methods don't try to access code files on disk, so the file does not have
        /// to exist.
        /// SharpDevelop itself uses internal names of the kind "[randomId]/Class1.cs" to support
        /// code-completion in unsaved files.
        /// </summary>
        public const string DummyFileName = "edited.cs";

        static readonly ICSharpCode.SharpDevelop.Dom.LanguageProperties CurrentLanguageProperties = IsVisualBasic ? ICSharpCode.SharpDevelop.Dom.LanguageProperties.VBNet : ICSharpCode.SharpDevelop.Dom.LanguageProperties.CSharp;

        public MainForm()
        {
            InitializeComponent();

            if (IsVisualBasic)
            {
                textEditorControl1.SetHighlighting("VBNET");
            }
            else
            {
                //textEditorControl1.Text = "";
                textEditorControl1.SetHighlighting("C#");
            }
            textEditorControl1.ShowEOLMarkers = false;
            textEditorControl1.ShowInvalidLines = false;
            HostCallbackImplementation.Register(this);
            CodeCompletionKeyHandler.Attach(this, textEditorControl1);
            ToolTipProvider.Attach(this, textEditorControl1);

            pcRegistry = new ICSharpCode.SharpDevelop.Dom.ProjectContentRegistry(); // Default .NET 2.0 registry

            // Persistence lets SharpDevelop.Dom create a cache file on disk so that
            // future starts are faster.
            // It also caches XML documentation files in an on-disk hash table, thus
            // reducing memory usage.
            pcRegistry.ActivatePersistence(Path.Combine(Path.GetTempPath(),
                                                        "CSharpCodeCompletion"));

            myProjectContent = new ICSharpCode.SharpDevelop.Dom.DefaultProjectContent();
            myProjectContent.Language = CurrentLanguageProperties;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            parserThread = new Thread(ParserThread);
            parserThread.IsBackground = true;
            parserThread.Start();
        }

        void ParserThread()
        {
            BeginInvoke(new MethodInvoker(delegate { parserThreadLabel.Text = "Loading mscorlib..."; }));
            myProjectContent.AddReferencedContent(pcRegistry.Mscorlib);

            // do one initial parser step to enable code-completion while other
            // references are loading
            ParseStep();

            string[] referencedAssemblies = {
                "System", "System.Data", "System.Drawing", "System.Xml", "System.Windows.Forms", "Microsoft.VisualBasic"
            };
            foreach (string assemblyName in referencedAssemblies)
            {
                string assemblyNameCopy = assemblyName; // copy for anonymous method
                BeginInvoke(new MethodInvoker(delegate { parserThreadLabel.Text = "Loading " + assemblyNameCopy + "..."; }));
                ICSharpCode.SharpDevelop.Dom.IProjectContent referenceProjectContent = pcRegistry.GetProjectContentForReference(assemblyName, assemblyName);
                myProjectContent.AddReferencedContent(referenceProjectContent);
                if (referenceProjectContent is ICSharpCode.SharpDevelop.Dom.ReflectionProjectContent)
                {
                    (referenceProjectContent as ICSharpCode.SharpDevelop.Dom.ReflectionProjectContent).InitializeReferences();
                }
            }
            if (IsVisualBasic)
            {
                myProjectContent.DefaultImports = new ICSharpCode.SharpDevelop.Dom.DefaultUsing(myProjectContent);
                myProjectContent.DefaultImports.Usings.Add("System");
                myProjectContent.DefaultImports.Usings.Add("System.Text");
                myProjectContent.DefaultImports.Usings.Add("Microsoft.VisualBasic");
            }
            BeginInvoke(new MethodInvoker(delegate { parserThreadLabel.Text = "Ready"; }));

            // Parse the current file every 2 seconds
            while (!IsDisposed)
            {
                ParseStep();

                Thread.Sleep(2000);
            }
        }

        void ParseStep()
        {
            string code = null;
            Invoke(new MethodInvoker(delegate {
                code = textEditorControl1.Text;
            }));
            TextReader textReader = new StringReader(code);
            ICSharpCode.SharpDevelop.Dom.ICompilationUnit newCompilationUnit;
            ICSharpCode.NRefactory.SupportedLanguage supportedLanguage;
            if (IsVisualBasic)
                supportedLanguage = ICSharpCode.NRefactory.SupportedLanguage.VBNet;
            else
                supportedLanguage = ICSharpCode.NRefactory.SupportedLanguage.CSharp;
            using (ICSharpCode.NRefactory.IParser p = ICSharpCode.NRefactory.ParserFactory.CreateParser(supportedLanguage, textReader))
            {
                // we only need to parse types and method definitions, no method bodies
                // so speed up the parser and make it more resistent to syntax
                // errors in methods
                p.ParseMethodBodies = false;

                p.Parse();
                newCompilationUnit = ConvertCompilationUnit(p.CompilationUnit);
            }
            // Remove information from lastCompilationUnit and add information from newCompilationUnit.
            myProjectContent.UpdateCompilationUnit(lastCompilationUnit, newCompilationUnit, DummyFileName);
            lastCompilationUnit = newCompilationUnit;
            parseInformation.SetCompilationUnit(newCompilationUnit);
        }

        ICSharpCode.SharpDevelop.Dom.ICompilationUnit ConvertCompilationUnit(ICSharpCode.NRefactory.Ast.CompilationUnit cu)
        {
            ICSharpCode.SharpDevelop.Dom.NRefactoryResolver.NRefactoryASTConvertVisitor converter;
            converter = new ICSharpCode.SharpDevelop.Dom.NRefactoryResolver.NRefactoryASTConvertVisitor(myProjectContent);
            cu.AcceptVisitor(converter, null);
            return converter.Cu;
        }
    }
}
