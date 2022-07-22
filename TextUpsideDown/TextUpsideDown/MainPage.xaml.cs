using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace TextUpsideDown
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        void OnButtonPressed(object sender, EventArgs args)
        {
            var text = input.Text;
            output.Text = Translate.InvertedText(text);
        }
    }
}
