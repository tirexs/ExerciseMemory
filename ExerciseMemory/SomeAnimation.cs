using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Animation;

namespace ExerciseMemory
{
    internal class SomeAnimation
    {
        public void Center_Screen(Window win)
        {
            double screenHeight = SystemParameters.FullPrimaryScreenHeight;
            double screenWidth = SystemParameters.FullPrimaryScreenWidth;


            win.Top = (screenHeight - win.Height) / 0x00000002;
            win.Left = (screenWidth - win.Width) / 0x00000002;

        }

        public void Opacity_Animation(Window win)
        {

            DoubleAnimation doubleAnimation = new DoubleAnimation
            {
                From = 0,
                To = 1,
                Duration = TimeSpan.FromSeconds(1.3)
            };
            win.BeginAnimation(Window.OpacityProperty, doubleAnimation);
        }

        public void Opacity_Animation(System.Windows.Controls.Label label)
        {

            DoubleAnimation doubleAnimation = new DoubleAnimation
            {
                From = 0,
                To = 1,
                Duration = TimeSpan.FromSeconds(3)
            };
           label.BeginAnimation(System.Windows.Controls.Label.OpacityProperty, doubleAnimation);
        }


        public void Opacity_Animation(System.Windows.Controls.Button button)
        {

            DoubleAnimation doubleAnimation = new DoubleAnimation
            {
                From = 0,
                To = 1,
                Duration = TimeSpan.FromSeconds(1.5)
            };
            button.BeginAnimation(System.Windows.Controls.Button.OpacityProperty, doubleAnimation);
        }

        public void Opacity_Animation(System.Windows.Controls.DataGrid datagrid)
        {

            DoubleAnimation doubleAnimation = new DoubleAnimation
            {
                From = 0,
                To = 1,
                Duration = TimeSpan.FromSeconds(1.5)
            };
            datagrid.BeginAnimation(System.Windows.Controls.DataGrid.OpacityProperty, doubleAnimation);
        }

        public void WidthAnimation(System.Windows.Controls.Button button, int width)
        {
            DoubleAnimation doubleAnimation = new DoubleAnimation
            {
                From = 0,
                To = width,
                Duration = TimeSpan.FromSeconds(2)
            };
            button.BeginAnimation(System.Windows.Controls.Button.WidthProperty, doubleAnimation);
        }


        public void WidthAnimation(System.Windows.Controls.TextBox text, int width)
        {
            DoubleAnimation doubleAnimation = new DoubleAnimation
            {
                From = 0,
                To = width,
                Duration = TimeSpan.FromSeconds(2)
            };
            text.BeginAnimation(System.Windows.Controls.TextBox.WidthProperty, doubleAnimation);
        }

    }
}
