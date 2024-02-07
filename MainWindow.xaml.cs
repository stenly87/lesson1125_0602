using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static System.Net.Mime.MediaTypeNames;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public byte[] Cat { get; set; }
        public DateTime CurrentDate { get; set; }
        public Uri Test { get; set; }
        public int CurrentValue { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            CurrentDate = DateTime.Now;
            Cat = File.ReadAllBytes("Images/Cat1.jpg");
            Test = new Uri("Любитель монет.mp4", UriKind.Relative);
            DataContext = this;
            
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Thread thread = new Thread(() =>
            {
                int i = 0;
                do
                {
                    if (i == 9)
                        Thread.Sleep(2500); // очень тяжелая нагрузка
                    Thread.Sleep(500); // обычная нагрузка
                    i++;
                    Dispatcher.Invoke(() => { // Диспетчер потока основного окна
                      // этот код выполняется в потоке основного окна
                      // обязательно, если идет обращение к компонентам в окне
                      progress.Value = i;
                      });
                    // Либо можно использовать привязку, но в некоторых случаях это может привести к ошибке, если не использовать Dispatcher
                    //CurrentValue = i;
                    //PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentValue)));

                }
                while (i < 10);
            });

            thread.Start();
        }
    }
}
