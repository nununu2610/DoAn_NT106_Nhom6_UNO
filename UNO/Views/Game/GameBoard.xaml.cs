using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace UNO.Views.Game
{
    public partial class GameBoard : Window
    {
        private List<string> allCardPaths = new List<string>();  // Danh sách tất cả các lá bài
        private List<Button> player1Buttons;  // Nút của player 1
        private List<Button> player2Buttons;  // Nút của player 2
        private Random random = new Random();  // Dùng để trộn các lá bài

        public GameBoard()
        {
            InitializeComponent();

            this.Loaded += (s, e) => MessageBox.Show("GameBoard Loaded");
            this.Closed += (s, e) => MessageBox.Show("GameBoard Closed");

            InitCardButtons();  // Khởi tạo danh sách các button cho player
            LoadCardPaths();  // Tải tất cả các đường dẫn hình ảnh lá bài
            DealCards();  // Chia bài cho các player
        }

        private void InitCardButtons()
        {
            // Gom các Button lại để dễ xử lý
            player1Buttons = new List<Button>
            {
                Player1Card1, Player1Card2, Player1Card3, Player1Card4,
                Player1Card5, Player1Card6, Player1Card7
            };

            player2Buttons = new List<Button>
            {
                Player2Card1, Player2Card2, Player2Card3, Player2Card4,
                Player2Card5, Player2Card6, Player2Card7
            };
        }

        private void LoadCardPaths()
        {
            string basePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images");
            string[] folders = { "BlueCard", "RedCard", "GreenCard", "YellowCard" };

            // Duyệt qua tất cả các thư mục và thêm tất cả hình ảnh vào danh sách
            foreach (string folder in folders)
            {
                string fullPath = Path.Combine(basePath, folder);
                if (Directory.Exists(fullPath))
                {
                    foreach (string file in Directory.GetFiles(fullPath, "*.png"))
                    {
                        allCardPaths.Add(file);
                    }
                }
            }
        }

        private void DealCards()
        {
            List<string> cards = new List<string>(allCardPaths);
            Shuffle(cards);  // Trộn bài

            if (cards.Count < 15)
            {
                MessageBox.Show($"Không đủ lá bài để bắt đầu trò chơi. Hiện có {cards.Count} lá.");
                this.Close();  // Đóng cửa sổ hoặc xử lý khác tùy bạn
                return;
            }

            // Chia bài cho player 1
            for (int i = 0; i < 7; i++)
            {
                SetButtonImage(player1Buttons[i], cards[i]);
            }

            // Chia bài cho player 2
            for (int i = 0; i < 7; i++)
            {
                SetButtonImage(player2Buttons[i], cards[7 + i]);
            }

            // Đặt 1 lá trên bàn
            SetTableCard(cards[14]);
        }

        private void Shuffle(List<string> list)
        {
            for (int i = list.Count - 1; i > 0; i--)
            {
                int j = random.Next(i + 1);
                (list[i], list[j]) = (list[j], list[i]);  // Hoán đổi 2 phần tử
            }
        }
        private void SetButtonImage(Button btn, string imagePath)
        {
            Image img = new Image
            {
                Source = new BitmapImage(new Uri(imagePath, UriKind.Absolute)),  // Đường dẫn đến ảnh
                Stretch = System.Windows.Media.Stretch.Fill  // Căng hình ảnh để vừa nút
            };
            btn.Content = img;  // Gán hình ảnh cho button
            btn.Tag = imagePath;  // Lưu đường dẫn hình ảnh để biết khi người chơi đánh lá nào
        }

        private void SetTableCard(string imagePath)
        {
            // Đặt lá bài lên bàn chơi
            TableCardImage.Source = new BitmapImage(new Uri(imagePath, UriKind.Absolute));
        }

        private void PlayCard_Click(object sender, RoutedEventArgs e)
        {
            // Lấy thông tin về nút bài đã nhấn
            if (sender is Button btn)
            {
                // Kiểm tra xem Tag có chứa giá trị hay không
                if (btn.Tag != null)
                {
                    string imagePath = btn.Tag as string;

                    // In ra đường dẫn hình ảnh trong Debug để kiểm tra
                    System.Diagnostics.Debug.WriteLine($"Card played: {imagePath}");

                    // Cập nhật bài đánh ra trên bàn
                    TableCardImage.Source = new BitmapImage(new Uri(imagePath, UriKind.Absolute));

                    // Vô hiệu hóa nút sau khi người chơi đánh bài
                    btn.IsEnabled = false;
                    btn.Opacity = 0.5; // Làm mờ nút sau khi đánh
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("Tag is null!");
                }
            }
        }

        private void DrawCardButton_Click(object sender, RoutedEventArgs e)
        {
            if (allCardPaths.Count == 0)
            {
                MessageBox.Show("No more cards to draw.");  // Thông báo nếu không còn bài để rút
                return;
            }

            // Rút 1 lá bài ngẫu nhiên
            string card = allCardPaths[random.Next(allCardPaths.Count)];

            // Tìm button trống đầu tiên của player 1 để gán bài mới
            foreach (var btn in player1Buttons)
            {
                if (btn.IsEnabled == false || btn.Content == null)
                {
                    SetButtonImage(btn, card);  // Đặt hình ảnh lá bài mới vào nút
                    btn.IsEnabled = true;  // Kích hoạt lại nút
                    btn.Opacity = 1;  // Đặt lại độ mờ của nút
                    break;
                }
            }
        }

        private void UNOButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("UNO!");  // Thông báo khi nhấn nút UNO
        }
    }
}
