using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocGenerate.Model.APIDoc
{
    public partial class WebViewMethod : Component
    {
        public WebViewMethod()
        {
            InitializeComponent();
        }

        public WebViewMethod(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }
    }
}
