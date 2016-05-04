using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        static FieldInfo GetFieldInfo(Type type,EventInfo eventinfo)
        {
            FieldInfo fieldInfo = type.GetField("EVENT_" + eventinfo.Name.ToUpper(), BindingFlags.NonPublic | BindingFlags.Static);

            if (fieldInfo == null && type.BaseType.ToString() != "System.ComponentModel.Component")
            {
                fieldInfo = GetFieldInfo(type.BaseType, eventinfo);
            }

            return fieldInfo;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            #region old
            //PropertyInfo propertyInfo = webBrowser1.GetType().GetProperty("Events", BindingFlags.NonPublic
            //                                                        | BindingFlags.Static | BindingFlags.Instance);
            //EventHandlerList eventHandlerList = propertyInfo.GetValue(webBrowser1, new object[] { }) as EventHandlerList;

            //FieldInfo fieldInfo;
            //StringBuilder sb = new StringBuilder();

            //EventInfo[] events = webBrowser1.GetType().GetEvents();

            //FieldInfo[] fieldInfo1 = typeof(WebBrowser).GetFields(BindingFlags.NonPublic | BindingFlags.Static);


            //for (int p = 0; p < fieldInfo1.Length; p++)
            //{
            //    if (fieldInfo1[p].Name.Contains("doc"))
            //    {
            //        MessageBox.Show("asd");
            //    }
            //}

            //for (int i = 0; i < events.Length; i++)
            //{
            //    if (events[i].Name == "CheckedChanged"|| events[i].Name == "CheckStateChanged" || events[i].Name == "AppearanceChanged"
            //        || events[i].Name == "DropDown" || events[i].Name == "DrawItem" || events[i].Name == "MeasureItem"
            //        || events[i].Name == "SelectedIndexChanged" || events[i].Name == "SelectionChangeCommitted"|| events[i].Name.Contains("Format")
            //        || events[i].Name == "DropDownClosed" || events[i].Name.Contains("ValueMember") || events[i].Name.Contains("DataSource")
            //        || events[i].Name.Contains("DisplayMember") || events[i].Name.Contains("DropDownStyle") || events[i].Name == "TextUpdate"
            //        || events[i].Name.Contains("FormatString") || events[i].Name.Contains("FormattingEnabled") || events[i].Name.Contains("SelectedIndex")
            //        || events[i].Name.Contains("SelectedValue")|| events[i].Name.Contains("TextAlignChanged")|| events[i].Name.Contains("ReadOnlyChanged")
            //        || events[i].Name.Contains("MultilineChanged") || events[i].Name.Contains("ModifiedChanged") || events[i].Name.Contains("HideSelectionChanged")
            //        || events[i].Name.Contains("BorderStyleChanged") || events[i].Name.Contains("AcceptsTabChanged"))
            //    {
            //        //递归（拿到fieldinfo）
            //        //ex:checkedListBox->ListBox->ListControl->Control
            //        fieldInfo = GetFieldInfo(typeof(WebBrowser),events[i]);
            //    }
            //    else
            //    {
            //        if (events[i].Name == "BackgroundImageChanged" || events[i].Name == "BackgroundImageLayoutChanged" ||
            //            events[i].Name.Contains("BackColor") || events[i].Name.Contains("BindingContext") || events[i].Name.Contains("CausesValidation")
            //            || events[i].Name == "SizeChanged" || events[i].Name.Contains("ContextMenuStrip") || events[i].Name.Contains("Cursor")
            //            || events[i].Name.Contains("Dock") || events[i].Name.Contains("Enabled") || events[i].Name.Contains("Font")
            //            || events[i].Name.Contains("ForeColor") || events[i].Name.Contains("Location")
            //            || events[i].Name.Contains("Parent") || events[i].Name.Contains("RightToLeft")
            //            || events[i].Name.Contains("TabIndex") || events[i].Name.Contains("TabStop") || events[i].Name.Contains("Text")
            //            || events[i].Name.Contains("Visible") || events[i].Name.Contains("ChangeUICues") || events[i].Name.Contains("ClientSize"))
            //        {
            //            fieldInfo = typeof(Control).GetField("Event" + events[i].Name.Replace("Changed", ""), BindingFlags.NonPublic | BindingFlags.Static);
            //        }
            //        else
            //        {
            //            fieldInfo = typeof(Control).GetField("Event" + events[i].Name, BindingFlags.NonPublic | BindingFlags.Static);

            //        }
            //    }

            //    if (fieldInfo == null)
            //    {
            //        continue;
            //    }
            //    var eventKey = fieldInfo.GetValue(webBrowser1);
            //    var eventHandler = eventHandlerList[eventKey] as Delegate;
            //    if (eventHandler == null)
            //    {
            //        continue;
            //    }
            //    Delegate[] invocationList = eventHandler.GetInvocationList();



            //    foreach (var handler in invocationList)
            //    {
            //        sb.Append(handler.GetMethodInfo().Name+"_"+i + "\r\n");
            //    }
            //}

            //textBox1.Text = sb.ToString();
            #endregion

            string[] array = {"as","asdf","asdfg","sdf" };
            string str = "as";
            if (Array.IndexOf<string>(array, str) != -1)
            {

            }

        }

        private void checkBox1_BackgroundImageLayoutChanged(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            MessageBox.Show("asdf");
        }

        private void checkBox1_BackgroundImageChanged(object sender, EventArgs e)
        {

        }

        private void checkBox1_Click(object sender, EventArgs e)
        {

        }

        private void checkBox1_AppearanceChanged(object sender, EventArgs e)
        {

        }

        private void checkBox1_AutoSizeChanged(object sender, EventArgs e)
        {

        }

        private void checkBox1_BackColorChanged(object sender, EventArgs e)
        {

        }

        private void checkBox1_BindingContextChanged(object sender, EventArgs e)
        {

        }

        private void checkBox1_CausesValidationChanged(object sender, EventArgs e)
        {

        }

        private void checkBox1_ChangeUICues(object sender, UICuesEventArgs e)
        {

        }

        private void checkBox1_CheckStateChanged(object sender, EventArgs e)
        {

        }

        private void checkBox1_ClientSizeChanged(object sender, EventArgs e)
        {

        }

        private void checkBox1_ContextMenuStripChanged(object sender, EventArgs e)
        {

        }

        private void checkBox1_ControlAdded(object sender, ControlEventArgs e)
        {

        }

        private void checkBox1_ControlRemoved(object sender, ControlEventArgs e)
        {

        }

        private void checkBox1_DockChanged(object sender, EventArgs e)
        {

        }

        private void checkBox1_DragDrop(object sender, DragEventArgs e)
        {

        }

        private void checkBox1_DragEnter(object sender, DragEventArgs e)
        {

        }

        private void checkBox1_DragLeave(object sender, EventArgs e)
        {

        }

        private void checkBox1_DragOver(object sender, DragEventArgs e)
        {

        }

        private void checkBox1_EnabledChanged(object sender, EventArgs e)
        {

        }

        private void checkBox1_Enter(object sender, EventArgs e)
        {

        }

        private void checkBox1_FontChanged(object sender, EventArgs e)
        {

        }

        private void checkBox1_GiveFeedback(object sender, GiveFeedbackEventArgs e)
        {

        }

        private void checkBox1_HelpRequested(object sender, HelpEventArgs hlpevent)
        {

        }

        private void checkBox1_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void checkBox1_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void checkBox1_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void checkBox1_Layout(object sender, LayoutEventArgs e)
        {

        }

        private void checkBox1_Leave(object sender, EventArgs e)
        {

        }

        private void checkBox1_LocationChanged(object sender, EventArgs e)
        {

        }

        private void checkBox1_MarginChanged(object sender, EventArgs e)
        {

        }

        private void checkBox1_MouseCaptureChanged(object sender, EventArgs e)
        {

        }

        private void checkBox1_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void checkBox1_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void checkBox1_MouseEnter(object sender, EventArgs e)
        {

        }

        private void checkBox1_MouseHover(object sender, EventArgs e)
        {

        }

        private void checkBox1_MouseLeave(object sender, EventArgs e)
        {

        }

        private void checkBox1_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void checkBox1_MouseUp(object sender, MouseEventArgs e)
        {

        }

        private void checkBox1_Move(object sender, EventArgs e)
        {

        }

        private void checkBox1_PaddingChanged(object sender, EventArgs e)
        {

        }

        private void checkBox1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void checkBox1_ParentChanged(object sender, EventArgs e)
        {

        }

        private void checkBox1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {

        }

        private void checkBox1_QueryAccessibilityHelp(object sender, QueryAccessibilityHelpEventArgs e)
        {

        }

        private void checkBox1_QueryContinueDrag(object sender, QueryContinueDragEventArgs e)
        {

        }

        private void checkBox1_RegionChanged(object sender, EventArgs e)
        {

        }

        private void checkBox1_Resize(object sender, EventArgs e)
        {

        }

        private void checkBox1_RightToLeftChanged(object sender, EventArgs e)
        {

        }

        private void checkBox1_SizeChanged(object sender, EventArgs e)
        {

        }

        private void checkBox1_StyleChanged(object sender, EventArgs e)
        {

        }

        private void checkBox1_SystemColorsChanged(object sender, EventArgs e)
        {

        }

        private void checkBox1_TabIndexChanged(object sender, EventArgs e)
        {

        }

        private void checkBox1_TabStopChanged(object sender, EventArgs e)
        {

        }

        private void checkBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void checkBox1_Validated(object sender, EventArgs e)
        {

        }

        private void checkBox1_Validating(object sender, CancelEventArgs e)
        {

        }

        private void checkBox1_VisibleChanged(object sender, EventArgs e)
        {

        }

        private void checkBox1_CursorChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_AutoSizeChanged(object sender, EventArgs e)
        {

        }

        private void button1_BackColorChanged(object sender, EventArgs e)
        {

        }

        private void button1_BackgroundImageChanged(object sender, EventArgs e)
        {

        }

        private void button1_BackgroundImageLayoutChanged(object sender, EventArgs e)
        {

        }

        private void button1_BindingContextChanged(object sender, EventArgs e)
        {

        }

        private void button1_CausesValidationChanged(object sender, EventArgs e)
        {

        }

        private void button1_ChangeUICues(object sender, UICuesEventArgs e)
        {

        }

        private void button1_ClientSizeChanged(object sender, EventArgs e)
        {

        }

        private void button1_ContextMenuStripChanged(object sender, EventArgs e)
        {

        }

        private void button1_ControlAdded(object sender, ControlEventArgs e)
        {

        }

        private void button1_ControlRemoved(object sender, ControlEventArgs e)
        {

        }

        private void button1_CursorChanged(object sender, EventArgs e)
        {

        }

        private void button1_DockChanged(object sender, EventArgs e)
        {

        }

        private void button1_DragDrop(object sender, DragEventArgs e)
        {

        }

        private void button1_DragEnter(object sender, DragEventArgs e)
        {

        }

        private void button1_DragLeave(object sender, EventArgs e)
        {

        }

        private void button1_DragOver(object sender, DragEventArgs e)
        {

        }

        private void button1_EnabledChanged(object sender, EventArgs e)
        {

        }

        private void button1_Enter(object sender, EventArgs e)
        {

        }

        private void button1_FontChanged(object sender, EventArgs e)
        {

        }

        private void button1_ForeColorChanged(object sender, EventArgs e)
        {

        }

        private void button1_GiveFeedback(object sender, GiveFeedbackEventArgs e)
        {

        }

        private void button1_HelpRequested(object sender, HelpEventArgs hlpevent)
        {

        }

        private void button1_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void button1_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void button1_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void button1_Layout(object sender, LayoutEventArgs e)
        {

        }

        private void button1_Leave(object sender, EventArgs e)
        {

        }

        private void button1_LocationChanged(object sender, EventArgs e)
        {

        }

        private void button1_MarginChanged(object sender, EventArgs e)
        {

        }

        private void button1_MouseCaptureChanged(object sender, EventArgs e)
        {

        }

        private void button1_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void button1_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void button1_MouseEnter(object sender, EventArgs e)
        {

        }

        private void button1_MouseHover(object sender, EventArgs e)
        {

        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {

        }

        private void button1_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void button1_MouseUp(object sender, MouseEventArgs e)
        {

        }

        private void button1_Move(object sender, EventArgs e)
        {

        }

        private void button1_PaddingChanged(object sender, EventArgs e)
        {

        }

        private void button1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_ParentChanged(object sender, EventArgs e)
        {

        }

        private void button1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {

        }

        private void button1_QueryAccessibilityHelp(object sender, QueryAccessibilityHelpEventArgs e)
        {

        }

        private void button1_QueryContinueDrag(object sender, QueryContinueDragEventArgs e)
        {

        }

        private void button1_RegionChanged(object sender, EventArgs e)
        {

        }

        private void button1_Resize(object sender, EventArgs e)
        {

        }

        private void button1_RightToLeftChanged(object sender, EventArgs e)
        {

        }

        private void button1_SizeChanged(object sender, EventArgs e)
        {

        }

        private void button1_StyleChanged(object sender, EventArgs e)
        {

        }

        private void button1_SystemColorsChanged(object sender, EventArgs e)
        {

        }

        private void button1_TabIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_TabStopChanged(object sender, EventArgs e)
        {

        }

        private void button1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Validated(object sender, EventArgs e)
        {

        }

        private void button1_Validating(object sender, CancelEventArgs e)
        {

        }

        private void button1_VisibleChanged(object sender, EventArgs e)
        {

        }

        private void radioButton1_AppearanceChanged(object sender, EventArgs e)
        {

        }

        private void radioButton1_AutoSizeChanged(object sender, EventArgs e)
        {

        }

        private void radioButton1_BackColorChanged(object sender, EventArgs e)
        {

        }

        private void radioButton1_BackgroundImageChanged(object sender, EventArgs e)
        {

        }

        private void radioButton1_BackgroundImageLayoutChanged(object sender, EventArgs e)
        {

        }

        private void radioButton1_BindingContextChanged(object sender, EventArgs e)
        {

        }

        private void radioButton1_CausesValidationChanged(object sender, EventArgs e)
        {

        }

        private void radioButton1_ChangeUICues(object sender, UICuesEventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton1_Click(object sender, EventArgs e)
        {

        }

        private void radioButton1_ClientSizeChanged(object sender, EventArgs e)
        {

        }

        private void radioButton1_ContextMenuStripChanged(object sender, EventArgs e)
        {

        }

        private void radioButton1_ControlAdded(object sender, ControlEventArgs e)
        {

        }

        private void radioButton1_ControlRemoved(object sender, ControlEventArgs e)
        {

        }

        private void radioButton1_CursorChanged(object sender, EventArgs e)
        {

        }

        private void radioButton1_DockChanged(object sender, EventArgs e)
        {

        }

        private void radioButton1_DragDrop(object sender, DragEventArgs e)
        {

        }

        private void radioButton1_DragEnter(object sender, DragEventArgs e)
        {

        }

        private void radioButton1_DragLeave(object sender, EventArgs e)
        {

        }

        private void radioButton1_DragOver(object sender, DragEventArgs e)
        {

        }

        private void radioButton1_EnabledChanged(object sender, EventArgs e)
        {

        }

        private void radioButton1_Enter(object sender, EventArgs e)
        {

        }

        private void radioButton1_FontChanged(object sender, EventArgs e)
        {

        }

        private void radioButton1_ForeColorChanged(object sender, EventArgs e)
        {

        }

        private void radioButton1_GiveFeedback(object sender, GiveFeedbackEventArgs e)
        {

        }

        private void radioButton1_HelpRequested(object sender, HelpEventArgs hlpevent)
        {

        }

        private void radioButton1_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void radioButton1_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void radioButton1_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void radioButton1_Layout(object sender, LayoutEventArgs e)
        {

        }

        private void radioButton1_Leave(object sender, EventArgs e)
        {

        }

        private void radioButton1_LocationChanged(object sender, EventArgs e)
        {

        }

        private void radioButton1_MarginChanged(object sender, EventArgs e)
        {

        }

        private void radioButton1_MouseCaptureChanged(object sender, EventArgs e)
        {

        }

        private void radioButton1_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void radioButton1_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void radioButton1_MouseEnter(object sender, EventArgs e)
        {

        }

        private void radioButton1_MouseHover(object sender, EventArgs e)
        {

        }

        private void radioButton1_MouseLeave(object sender, EventArgs e)
        {

        }

        private void radioButton1_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void radioButton1_MouseUp(object sender, MouseEventArgs e)
        {

        }

        private void radioButton1_Move(object sender, EventArgs e)
        {

        }

        private void radioButton1_PaddingChanged(object sender, EventArgs e)
        {

        }

        private void radioButton1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void radioButton1_ParentChanged(object sender, EventArgs e)
        {

        }

        private void radioButton1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {

        }

        private void radioButton1_QueryAccessibilityHelp(object sender, QueryAccessibilityHelpEventArgs e)
        {

        }

        private void radioButton1_QueryContinueDrag(object sender, QueryContinueDragEventArgs e)
        {

        }

        private void radioButton1_RegionChanged(object sender, EventArgs e)
        {

        }

        private void radioButton1_Resize(object sender, EventArgs e)
        {

        }

        private void radioButton1_RightToLeftChanged(object sender, EventArgs e)
        {

        }

        private void radioButton1_SizeChanged(object sender, EventArgs e)
        {

        }

        private void radioButton1_StyleChanged(object sender, EventArgs e)
        {

        }

        private void radioButton1_SystemColorsChanged(object sender, EventArgs e)
        {

        }

        private void radioButton1_TabIndexChanged(object sender, EventArgs e)
        {

        }

        private void radioButton1_TabStopChanged(object sender, EventArgs e)
        {

        }

        private void radioButton1_TextChanged(object sender, EventArgs e)
        {

        }

        private void radioButton1_Validated(object sender, EventArgs e)
        {

        }

        private void radioButton1_Validating(object sender, CancelEventArgs e)
        {

        }

        private void radioButton1_VisibleChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_BackColorChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_BindingContextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_CausesValidationChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_ChangeUICues(object sender, UICuesEventArgs e)
        {

        }

        private void comboBox1_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_ClientSizeChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_ContextMenuStripChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_ControlAdded(object sender, ControlEventArgs e)
        {

        }

        private void comboBox1_ControlRemoved(object sender, ControlEventArgs e)
        {

        }

        private void comboBox1_CursorChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_DataSourceChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_DisplayMemberChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_DockChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_DragDrop(object sender, DragEventArgs e)
        {

        }

        private void comboBox1_DragEnter(object sender, DragEventArgs e)
        {

        }

        private void comboBox1_DragLeave(object sender, EventArgs e)
        {

        }

        private void comboBox1_DragOver(object sender, DragEventArgs e)
        {

        }

        private void comboBox1_DrawItem(object sender, DrawItemEventArgs e)
        {

        }

        private void comboBox1_DropDown(object sender, EventArgs e)
        {

        }

        private void comboBox1_DropDownClosed(object sender, EventArgs e)
        {

        }

        private void comboBox1_DropDownStyleChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_EnabledChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_Enter(object sender, EventArgs e)
        {

        }

        private void comboBox1_FontChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_ForeColorChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_Format(object sender, ListControlConvertEventArgs e)
        {

        }

        private void comboBox1_FormatStringChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_FormattingEnabledChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_GiveFeedback(object sender, GiveFeedbackEventArgs e)
        {

        }

        private void comboBox1_HelpRequested(object sender, HelpEventArgs hlpevent)
        {

        }

        private void comboBox1_ImeModeChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void comboBox1_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void comboBox1_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void comboBox1_Layout(object sender, LayoutEventArgs e)
        {

        }

        private void comboBox1_Leave(object sender, EventArgs e)
        {

        }

        private void comboBox1_LocationChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_MarginChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_MeasureItem(object sender, MeasureItemEventArgs e)
        {

        }

        private void comboBox1_MouseCaptureChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void comboBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {

        }

        private void comboBox1_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void comboBox1_MouseEnter(object sender, EventArgs e)
        {

        }

        private void comboBox1_MouseHover(object sender, EventArgs e)
        {

        }

        private void comboBox1_MouseLeave(object sender, EventArgs e)
        {

        }

        private void comboBox1_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void comboBox1_MouseUp(object sender, MouseEventArgs e)
        {

        }

        private void comboBox1_Move(object sender, EventArgs e)
        {

        }

        private void comboBox1_ParentChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {

        }

        private void comboBox1_QueryAccessibilityHelp(object sender, QueryAccessibilityHelpEventArgs e)
        {

        }

        private void comboBox1_QueryContinueDrag(object sender, QueryContinueDragEventArgs e)
        {

        }

        private void comboBox1_RegionChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_Resize(object sender, EventArgs e)
        {

        }

        private void comboBox1_RightToLeftChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {

        }

        private void comboBox1_SizeChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_StyleChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SystemColorsChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_TabIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_TabStopChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_TextUpdate(object sender, EventArgs e)
        {

        }

        private void comboBox1_Validated(object sender, EventArgs e)
        {

        }

        private void comboBox1_Validating(object sender, CancelEventArgs e)
        {

        }

        private void comboBox1_ValueMemberChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_VisibleChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_AcceptsTabChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_BackColorChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_BindingContextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_BorderStyleChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_CausesValidationChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_ChangeUICues(object sender, UICuesEventArgs e)
        {

        }

        private void textBox2_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_ClientSizeChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_ContextMenuStripChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_ControlAdded(object sender, ControlEventArgs e)
        {

        }

        private void textBox2_ControlRemoved(object sender, ControlEventArgs e)
        {

        }

        private void textBox2_CursorChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_DockChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_DoubleClick(object sender, EventArgs e)
        {

        }

        private void textBox2_DragDrop(object sender, DragEventArgs e)
        {

        }

        private void textBox2_DragEnter(object sender, DragEventArgs e)
        {

        }

        private void textBox2_DragLeave(object sender, EventArgs e)
        {

        }

        private void textBox2_DragOver(object sender, DragEventArgs e)
        {

        }

        private void textBox2_EnabledChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_Enter(object sender, EventArgs e)
        {

        }

        private void textBox2_FontChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_ForeColorChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_GiveFeedback(object sender, GiveFeedbackEventArgs e)
        {

        }

        private void textBox2_HelpRequested(object sender, HelpEventArgs hlpevent)
        {

        }

        private void textBox2_HideSelectionChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_ImeModeChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void textBox2_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void textBox2_Layout(object sender, LayoutEventArgs e)
        {

        }

        private void textBox2_Leave(object sender, EventArgs e)
        {

        }

        private void textBox2_LocationChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_MarginChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_ModifiedChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_MouseCaptureChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void textBox2_MouseDoubleClick(object sender, MouseEventArgs e)
        {

        }

        private void textBox2_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void textBox2_MouseEnter(object sender, EventArgs e)
        {

        }

        private void textBox2_MouseHover(object sender, EventArgs e)
        {

        }

        private void textBox2_MouseLeave(object sender, EventArgs e)
        {

        }

        private void textBox2_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void textBox2_MouseUp(object sender, MouseEventArgs e)
        {

        }

        private void textBox2_Move(object sender, EventArgs e)
        {

        }

        private void textBox2_MultilineChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_ParentChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {

        }

        private void textBox2_QueryAccessibilityHelp(object sender, QueryAccessibilityHelpEventArgs e)
        {

        }

        private void textBox2_QueryContinueDrag(object sender, QueryContinueDragEventArgs e)
        {

        }

        private void textBox2_ReadOnlyChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_RegionChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_Resize(object sender, EventArgs e)
        {

        }

        private void textBox2_RightToLeftChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_SizeChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_StyleChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_SystemColorsChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TabIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TabStopChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextAlignChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_Validated(object sender, EventArgs e)
        {

        }

        private void textBox2_Validating(object sender, CancelEventArgs e)
        {

        }

        private void textBox2_VisibleChanged(object sender, EventArgs e)
        {

        }

        private void label1_AutoSizeChanged(object sender, EventArgs e)
        {

        }

        private void label1_BackColorChanged(object sender, EventArgs e)
        {

        }

        private void label1_BindingContextChanged(object sender, EventArgs e)
        {

        }

        private void label1_CausesValidationChanged(object sender, EventArgs e)
        {

        }

        private void label1_ChangeUICues(object sender, UICuesEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label1_ClientSizeChanged(object sender, EventArgs e)
        {

        }

        private void label1_ContextMenuStripChanged(object sender, EventArgs e)
        {

        }

        private void label1_ControlAdded(object sender, ControlEventArgs e)
        {

        }

        private void label1_ControlRemoved(object sender, ControlEventArgs e)
        {

        }

        private void label1_CursorChanged(object sender, EventArgs e)
        {

        }

        private void label1_DockChanged(object sender, EventArgs e)
        {

        }

        private void label1_DoubleClick(object sender, EventArgs e)
        {

        }

        private void label1_DragDrop(object sender, DragEventArgs e)
        {

        }

        private void label1_DragEnter(object sender, DragEventArgs e)
        {

        }

        private void label1_DragLeave(object sender, EventArgs e)
        {

        }

        private void label1_DragOver(object sender, DragEventArgs e)
        {

        }

        private void label1_EnabledChanged(object sender, EventArgs e)
        {

        }

        private void label1_Enter(object sender, EventArgs e)
        {

        }

        private void label1_FontChanged(object sender, EventArgs e)
        {

        }

        private void label1_ForeColorChanged(object sender, EventArgs e)
        {

        }

        private void label1_GiveFeedback(object sender, GiveFeedbackEventArgs e)
        {

        }

        private void label1_HelpRequested(object sender, HelpEventArgs hlpevent)
        {

        }

        private void label1_Layout(object sender, LayoutEventArgs e)
        {

        }

        private void label1_Leave(object sender, EventArgs e)
        {

        }

        private void label1_LocationChanged(object sender, EventArgs e)
        {

        }

        private void label1_MarginChanged(object sender, EventArgs e)
        {

        }

        private void label1_MouseCaptureChanged(object sender, EventArgs e)
        {

        }

        private void label1_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void label1_MouseDoubleClick(object sender, MouseEventArgs e)
        {

        }

        private void label1_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void label1_MouseEnter(object sender, EventArgs e)
        {

        }

        private void label1_MouseHover(object sender, EventArgs e)
        {

        }

        private void label1_MouseLeave(object sender, EventArgs e)
        {

        }

        private void label1_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void label1_MouseUp(object sender, MouseEventArgs e)
        {

        }

        private void label1_Move(object sender, EventArgs e)
        {

        }

        private void label1_PaddingChanged(object sender, EventArgs e)
        {

        }

        private void label1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_ParentChanged(object sender, EventArgs e)
        {

        }

        private void label1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {

        }

        private void label1_QueryAccessibilityHelp(object sender, QueryAccessibilityHelpEventArgs e)
        {

        }

        private void label1_QueryContinueDrag(object sender, QueryContinueDragEventArgs e)
        {

        }

        private void label1_RegionChanged(object sender, EventArgs e)
        {

        }

        private void label1_Resize(object sender, EventArgs e)
        {

        }

        private void label1_RightToLeftChanged(object sender, EventArgs e)
        {

        }

        private void label1_SizeChanged(object sender, EventArgs e)
        {

        }

        private void label1_StyleChanged(object sender, EventArgs e)
        {

        }

        private void label1_SystemColorsChanged(object sender, EventArgs e)
        {

        }

        private void label1_TabIndexChanged(object sender, EventArgs e)
        {

        }

        private void label1_TextAlignChanged(object sender, EventArgs e)
        {

        }

        private void label1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Validated(object sender, EventArgs e)
        {

        }

        private void label1_Validating(object sender, CancelEventArgs e)
        {

        }

        private void label1_VisibleChanged(object sender, EventArgs e)
        {

        }

        private void checkedListBox1_BackColorChanged(object sender, EventArgs e)
        {

        }

        private void checkedListBox1_BindingContextChanged(object sender, EventArgs e)
        {

        }

        private void checkedListBox1_CausesValidationChanged(object sender, EventArgs e)
        {

        }

        private void checkedListBox1_ChangeUICues(object sender, UICuesEventArgs e)
        {

        }

        private void checkedListBox1_Click(object sender, EventArgs e)
        {

        }

        private void checkedListBox1_ClientSizeChanged(object sender, EventArgs e)
        {

        }

        private void checkedListBox1_ContextMenuStripChanged(object sender, EventArgs e)
        {

        }

        private void checkedListBox1_ControlAdded(object sender, ControlEventArgs e)
        {

        }

        private void checkedListBox1_ControlRemoved(object sender, ControlEventArgs e)
        {

        }

        private void checkedListBox1_CursorChanged(object sender, EventArgs e)
        {

        }

        private void checkedListBox1_DockChanged(object sender, EventArgs e)
        {

        }

        private void checkedListBox1_DoubleClick(object sender, EventArgs e)
        {

        }

        private void checkedListBox1_DragDrop(object sender, DragEventArgs e)
        {

        }

        private void checkedListBox1_DragEnter(object sender, DragEventArgs e)
        {

        }

        private void checkedListBox1_DragLeave(object sender, EventArgs e)
        {

        }

        private void checkedListBox1_DragOver(object sender, DragEventArgs e)
        {

        }

        private void checkedListBox1_EnabledChanged(object sender, EventArgs e)
        {

        }

        private void checkedListBox1_Enter(object sender, EventArgs e)
        {

        }

        private void checkedListBox1_FontChanged(object sender, EventArgs e)
        {

        }

        private void checkedListBox1_ForeColorChanged(object sender, EventArgs e)
        {

        }

        private void checkedListBox1_Format(object sender, ListControlConvertEventArgs e)
        {

        }

        private void checkedListBox1_FormatStringChanged(object sender, EventArgs e)
        {

        }

        private void checkedListBox1_FormattingEnabledChanged(object sender, EventArgs e)
        {

        }

        private void checkedListBox1_GiveFeedback(object sender, GiveFeedbackEventArgs e)
        {

        }

        private void checkedListBox1_HelpRequested(object sender, HelpEventArgs hlpevent)
        {

        }

        private void checkedListBox1_ImeModeChanged(object sender, EventArgs e)
        {

        }

        private void checkedListBox1_ItemCheck(object sender, ItemCheckEventArgs e)
        {

        }

        private void checkedListBox1_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void checkedListBox1_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void checkedListBox1_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void checkedListBox1_Layout(object sender, LayoutEventArgs e)
        {

        }

        private void checkedListBox1_Leave(object sender, EventArgs e)
        {

        }

        private void checkedListBox1_LocationChanged(object sender, EventArgs e)
        {

        }

        private void checkedListBox1_MarginChanged(object sender, EventArgs e)
        {

        }

        private void checkedListBox1_MouseCaptureChanged(object sender, EventArgs e)
        {

        }

        private void checkedListBox1_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void checkedListBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {

        }

        private void checkedListBox1_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void checkedListBox1_MouseEnter(object sender, EventArgs e)
        {

        }

        private void checkedListBox1_MouseHover(object sender, EventArgs e)
        {

        }

        private void checkedListBox1_MouseLeave(object sender, EventArgs e)
        {

        }

        private void checkedListBox1_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void checkedListBox1_MouseUp(object sender, MouseEventArgs e)
        {

        }

        private void checkedListBox1_Move(object sender, EventArgs e)
        {

        }

        private void checkedListBox1_ParentChanged(object sender, EventArgs e)
        {

        }

        private void checkedListBox1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {

        }

        private void checkedListBox1_QueryAccessibilityHelp(object sender, QueryAccessibilityHelpEventArgs e)
        {

        }

        private void checkedListBox1_QueryContinueDrag(object sender, QueryContinueDragEventArgs e)
        {

        }

        private void checkedListBox1_RegionChanged(object sender, EventArgs e)
        {

        }

        private void checkedListBox1_Resize(object sender, EventArgs e)
        {

        }

        private void checkedListBox1_RightToLeftChanged(object sender, EventArgs e)
        {

        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void checkedListBox1_SelectedValueChanged(object sender, EventArgs e)
        {

        }

        private void checkedListBox1_SizeChanged(object sender, EventArgs e)
        {

        }

        private void checkedListBox1_StyleChanged(object sender, EventArgs e)
        {

        }

        private void checkedListBox1_SystemColorsChanged(object sender, EventArgs e)
        {

        }

        private void checkedListBox1_TabIndexChanged(object sender, EventArgs e)
        {

        }

        private void checkedListBox1_TabStopChanged(object sender, EventArgs e)
        {

        }

        private void checkedListBox1_Validated(object sender, EventArgs e)
        {

        }

        private void checkedListBox1_Validating(object sender, CancelEventArgs e)
        {

        }

        private void checkedListBox1_VisibleChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_BindingContextChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_CausesValidationChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ChangeUICues(object sender, UICuesEventArgs e)
        {

        }

        private void dateTimePicker1_ClientSizeChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_CloseUp(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ContextMenuStripChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ControlAdded(object sender, ControlEventArgs e)
        {

        }

        private void dateTimePicker1_ControlRemoved(object sender, ControlEventArgs e)
        {

        }

        private void dateTimePicker1_CursorChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_DockChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_DragDrop(object sender, DragEventArgs e)
        {

        }

        private void dateTimePicker1_DragEnter(object sender, DragEventArgs e)
        {

        }

        private void dateTimePicker1_DragLeave(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_DragOver(object sender, DragEventArgs e)
        {

        }

        private void dateTimePicker1_DropDown(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_EnabledChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_Enter(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_FontChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_FormatChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_GiveFeedback(object sender, GiveFeedbackEventArgs e)
        {

        }

        private void dateTimePicker1_HelpRequested(object sender, HelpEventArgs hlpevent)
        {

        }

        private void dateTimePicker1_ImeModeChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void dateTimePicker1_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void dateTimePicker1_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void dateTimePicker1_Layout(object sender, LayoutEventArgs e)
        {

        }

        private void dateTimePicker1_Leave(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_LocationChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_MarginChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_MouseCaptureChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void dateTimePicker1_MouseEnter(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_MouseHover(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_MouseLeave(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void dateTimePicker1_MouseUp(object sender, MouseEventArgs e)
        {

        }

        private void dateTimePicker1_Move(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ParentChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {

        }

        private void dateTimePicker1_QueryAccessibilityHelp(object sender, QueryAccessibilityHelpEventArgs e)
        {

        }

        private void dateTimePicker1_QueryContinueDrag(object sender, QueryContinueDragEventArgs e)
        {

        }

        private void dateTimePicker1_RegionChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_Resize(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_RightToLeftChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_RightToLeftLayoutChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_SizeChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_StyleChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_SystemColorsChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_TabIndexChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_TabStopChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_Validated(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_Validating(object sender, CancelEventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_VisibleChanged(object sender, EventArgs e)
        {

        }

        private void webBrowser1_CausesValidationChanged(object sender, EventArgs e)
        {

        }

        private void webBrowser1_ClientSizeChanged(object sender, EventArgs e)
        {

        }

        private void webBrowser1_ContextMenuStripChanged(object sender, EventArgs e)
        {

        }

        private void webBrowser1_ControlAdded(object sender, ControlEventArgs e)
        {

        }

        private void webBrowser1_ControlRemoved(object sender, ControlEventArgs e)
        {

        }

        private void webBrowser1_DockChanged(object sender, EventArgs e)
        {

        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {

        }

        private void webBrowser1_FileDownload(object sender, EventArgs e)
        {

        }

        private void webBrowser1_LocationChanged(object sender, EventArgs e)
        {

        }

        private void webBrowser1_MarginChanged(object sender, EventArgs e)
        {

        }

        private void webBrowser1_Move(object sender, EventArgs e)
        {

        }

        private void webBrowser1_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {

        }

        private void webBrowser1_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {

        }

        private void webBrowser1_NewWindow(object sender, CancelEventArgs e)
        {

        }

        private void webBrowser1_ParentChanged(object sender, EventArgs e)
        {

        }

        private void webBrowser1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {

        }

        private void webBrowser1_ProgressChanged(object sender, WebBrowserProgressChangedEventArgs e)
        {

        }

        private void webBrowser1_RegionChanged(object sender, EventArgs e)
        {

        }

        private void webBrowser1_Resize(object sender, EventArgs e)
        {

        }

        private void webBrowser1_SizeChanged(object sender, EventArgs e)
        {

        }

        private void webBrowser1_SystemColorsChanged(object sender, EventArgs e)
        {

        }

        private void webBrowser1_TabIndexChanged(object sender, EventArgs e)
        {

        }

        private void webBrowser1_TabStopChanged(object sender, EventArgs e)
        {

        }

        private void webBrowser1_Validated(object sender, EventArgs e)
        {

        }

        private void webBrowser1_Validating(object sender, CancelEventArgs e)
        {

        }

        private void webBrowser1_VisibleChanged(object sender, EventArgs e)
        {

        }
    }
}
