using System.Collections.Generic;
using System.Drawing;
using NUnit.Framework;
using Osherove.Controls;
using Regulazy.UI;
using Regulazy.UI.RichTextBoxCustom;

namespace RegexWizard.Framework.Tests
{
    [TestFixture]
    public class CustomRTBTests
    {
       [Test]
        public void GetSurroundingRects()
       {
           string text = @"A
B
C
D
E";
           List<Rectangle> rects = GetRects(text, 3, 6);
           Assert.AreEqual(4,rects.Count);
       }
        
        [Test]
        public void GetSurroundingRects2()
       {
           string text = @"1sfsdf
2sdf
3sf
4sdfsd
5
sdf";
            List<Rectangle> rects = GetRects(text, 15, 13);
            Assert.AreEqual(4,rects.Count);
       }

       

        [Test]
    public void GetSurrpundingRects_WidthOfFirstLineIsCorrect()
        {
            string text = @"This is some really really
 really 
really
 long sample text
sdf
sdf
sdfsdfdsfsd
sdf";

            List<Rectangle> rects = GetRects(text, 46, 38);
            Assert.IsTrue(rects[0].Width>50,"first line width was only {0} and not larger than 60 as needed from the line start",rects[0].Width);
        }
        
        private List<Rectangle> GetRects(string text, int startIndex, int length)
        {
            CustomDrawRTB rtb = new CustomDrawRTB();
            rtb.Font = new Font("Courier",16);
            rtb.Text = text;
            return rtb.GetSurroundingRects(startIndex, length);
        }
        
        
      
    }
}
