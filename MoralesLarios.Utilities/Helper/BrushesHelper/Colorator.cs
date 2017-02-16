using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace MoralesLarios.Utilities.Helper.BrushesHelper
{
    internal class Colorator
    {
        public void FlashColor(Control control, Color brush)
        {
            Storyboard sb = new Storyboard();
            ColorAnimation animation;
             
            // Animate the brush 
            animation = new ColorAnimation();
            animation.To = brush;
            animation.Duration = new Duration(TimeSpan.FromSeconds(0.1));
            Storyboard.SetTarget(animation, control);
            string property = GetPropertyBackGround(control);
            Storyboard.SetTargetProperty(animation, new PropertyPath(property));
            sb.Children.Add(animation);
            sb.AutoReverse = true;
            //control.Background.BeginAnimation(Control.BackgroundProperty, animation);
            sb.Begin(Application.Current.Windows[0]);
        }


        private string GetPropertyBackGround(Control control)
        {
            string result = "Background.Color";

            //var dataGrid = control as DataGrid;

            //if(dataGrid != null)
            //{
            //    result = "RowBackground.Color";
            //}

            return result;
        }



        //private object GetFirstParent(Control control)
        //{
        //    object 
        //}
    }
}
