using System;
using Windows.UI;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Media;

namespace SenpaiUtilities
{
    public static class TextUtilities
    {
        public static Run CreateRun(String text)
        {
            return new Run() { Text = text };
        }

        public static Run CreateRun(String text, Color foreground)
        {
            return new Run() { Text = text, Foreground = new SolidColorBrush(foreground) };
        }

        public static void SetExampleText(TextBlock textBlock1, TextBlock textBlock2, String example)
        {
            String[] parts = example.Split('<');

            foreach (String part in parts)
            {
                String[] subParts = part.Split('>');
                
                if (subParts.Length == 2)
                {
                    textBlock1.Inlines.Add(CreateRun("__"       , Colors.LimeGreen));
                    textBlock2.Inlines.Add(CreateRun(subParts[0], Colors.LimeGreen));

                    textBlock1.Inlines.Add(CreateRun(subParts[1]));
                    textBlock2.Inlines.Add(CreateRun(subParts[1]));
                }
                else
                {
                    textBlock1.Inlines.Add(CreateRun(part));
                    textBlock2.Inlines.Add(CreateRun(part));
                }
            }    
        }
    }
}
