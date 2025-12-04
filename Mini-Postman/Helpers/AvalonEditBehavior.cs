using System;
using System.Windows;
using ICSharpCode.AvalonEdit;

namespace Mini_Postman.Helpers;

public static class AvalonEditBehaviour
{
    public static readonly DependencyProperty TextProperty = DependencyProperty.RegisterAttached(
        "Text",
        typeof(string),
        typeof(AvalonEditBehaviour),
        new FrameworkPropertyMetadata(default(string), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, PropertyChangedCallback));

    public static string GetText(DependencyObject dependencyObject)
    {
        return (string)dependencyObject.GetValue(TextProperty);
    }

    public static void SetText(DependencyObject dependencyObject, string value)
    {
        dependencyObject.SetValue(TextProperty, value);
    }

    private static void PropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is TextEditor editor)
        {
            editor.TextChanged -= Editor_TextChanged;

            var newText = e.NewValue?.ToString() ?? "";
            
            if (editor.Document != null && editor.Document.Text != newText)
            {
                var caretOffset = editor.CaretOffset;
                editor.Document.Text = newText;
                
                if (caretOffset <= editor.Document.TextLength)
                    editor.CaretOffset = caretOffset;
            }
            
            editor.TextChanged += Editor_TextChanged;
        }
    }

    private static void Editor_TextChanged(object? sender, EventArgs e)
    {
        if (sender is TextEditor editor)
        {
            SetText(editor, editor.Document.Text);
        }
    }
}