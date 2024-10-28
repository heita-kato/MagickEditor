using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ImageMagick;
using OpenCvSharp;
using Windows.Graphics.Imaging;
using Windows.Media.Ocr;
using Windows.Storage.Streams;
using Windows.Storage;

using System.IO;
using System.Drawing.Imaging;
using System.Net;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Shapes;
using ImageMagick.Drawing;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;
using WindowsFormsApp2.Properties;
using static System.Net.Mime.MediaTypeNames;

namespace WindowsFormsApp2
{

    public partial class Form1 : Form
    {
        private MagickImage _image;
        private Color color0 = Color.WhiteSmoke;
        
        public Form1()
        {
            InitializeComponent();
            //this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
            //境界線スタイルをFixedToolWindowにする
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void openToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            
        }

        private void openToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    if (_image != null)
                    {
                        // 保存した画像ファイルの削除
                        //File.Delete(@"C:\test\1.bmp");
                        //後始末
                        _image = null;
                        //終了
                        //this.Close();
                        //PictureBox1から削除する
                        pictureBox1.Image = null;
                    }
                    LoadImage(openFileDialog.FileName);
                }
            }
        }

        private void LoadImage(string path)
        {
            _image = new MagickImage(path);
            //BMP形式で保存する
            _image.Write(@"C:\test\1.bmp");
            //PictureBox1に表示する
            pictureBox1.ImageLocation = @"C:\test\1.bmp";
            //label1にログを残す
            label1.ForeColor = Color.LawnGreen;
            label1.Text = "+ Image Opened";
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void rotateToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            
            
            
        }

        private void resizeToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            //textBox1.Text = "* cannot move";
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void grayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_image != null)
            {
                // 画像の読み込み
                Mat mat = new Mat(@"C:\test\1.bmp");
                Mat matGray = mat.CvtColor(ColorConversionCodes.BGR2GRAY);

                // 画像の保存
                Cv2.ImWrite(@"C:\test\1.bmp", matGray);
                //PictureBox1に表示する
                pictureBox1.ImageLocation = @"C:\test\1.bmp";
                //label1にログを残す
                label1.ForeColor = Color.LawnGreen;
                label1.Text = "+ Gray Scale";
            }
            else
            {
                //label1にログを残す
                label1.ForeColor = Color.Red;
                label1.Text = "- No Image!";
            }
        }

        private void bodyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_image != null)
            {
                // 画像とカスケード分類器の読み込み
                using (var mat = new Mat(@"C:\test\1.bmp"))
                using (var cascade = new CascadeClassifier(@"C:\test\CascadeClassifier\haarcascade_fullbody.xml"))
                {
                    // 検出
                    foreach (Rect rectDetect in cascade.DetectMultiScale(mat))
                    {
                        // 顔の領域をぼかす
                        var faceRegion = new Mat(mat, rectDetect);
                        int intVal3 = Decimal.ToInt32(numericUpDown3.Value);
                        Cv2.GaussianBlur(faceRegion, faceRegion, new OpenCvSharp.Size(Math.Abs(intVal3), Math.Abs(intVal3)), 0);
                        // 枠を表示
                        //Cv2.Rectangle(mat, rectDetect, new Scalar(255, 255, 0), 2);
                    }
                    // 画像の保存
                    Cv2.ImWrite(@"C:\test\1.bmp", mat);
                    //再読み込み
                    _image = new MagickImage(@"C:\test\1.bmp");
                    //ぼかし
                    //_image.Blur(5.0, 2.5);
                    //PictureBox1に表示する
                    pictureBox1.ImageLocation = @"C:\test\1.bmp";
                    //label1にログを残す
                    label1.ForeColor = Color.LawnGreen;
                    label1.Text = "+ Body Blured";
                }
            }
            else
            {
                //label1にログを残す
                label1.ForeColor = Color.Red;
                label1.Text = "- No Image!";
            }
        }

        private void faceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_image != null)
            {
                // 画像とカスケード分類器の読み込み
                using (var mat = new Mat(@"C:\test\1.bmp"))
                using (var cascade = new CascadeClassifier(@"C:\test\CascadeClassifier\haarcascade_frontalface_alt.xml"))
                {
                    // 検出
                    foreach (Rect rectDetect in cascade.DetectMultiScale(mat))
                    {
                        // 顔の領域をぼかす
                        var faceRegion = new Mat(mat, rectDetect);
                        int intVal3 = Decimal.ToInt32(numericUpDown3.Value);
                        Cv2.GaussianBlur(faceRegion, faceRegion, new OpenCvSharp.Size(Math.Abs(intVal3), Math.Abs(intVal3)), 0);
                        // 枠を表示
                        //Cv2.Rectangle(mat, rectDetect, new Scalar(255, 255, 0), 2);
                    }

                    //Cv2.GaussianBlur(mat, mat, new OpenCvSharp.Size(31, 31), 0);
                    // 画像の保存
                    Cv2.ImWrite(@"C:\test\1.bmp", mat);
                    //再読み込み
                    _image = new MagickImage(@"C:\test\1.bmp");
                    //ぼかし
                    //_image.Blur(5.0, 2.5);
                    //PictureBox1に表示する
                    pictureBox1.ImageLocation = @"C:\test\1.bmp";
                    //label1にログを残す
                    label1.ForeColor = Color.LawnGreen;
                    label1.Text = "+ Face Blured";
                }
            }
            else
            {
                //label1にログを残す
                label1.ForeColor = Color.Red;
                label1.Text = "- No Image!";
            }
        }

        private void tooToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void smallerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void largerToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void smallerToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            
        }

        private void largerToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            
        }

        private async void charactersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_image != null)
            {
                SoftwareBitmap softwareBitmap = await GetSoftwareSnapShot();
                OcrResult ocrResult = await RunWin10Ocr(softwareBitmap);  // softwareSnapはSoftwareBitmap形式の画像
                string ocrText = ocrResult.Text;   // OCR結果文字列
                                                   //textBox1.Text = ocrText;
                                                   //Rect rect = ocrResult.BoundingRect;
                richTextBox1.Text = ocrText;
                //label1にログを残す
                label1.ForeColor = Color.LawnGreen;
                label1.Text = "+ Read Text";
            }
            else
            {
                //label1にログを残す
                label1.ForeColor = Color.Red;
                label1.Text = "- No Image!";
            }
        }

        // 取得した画像のテキストを抽出する(Windows10 OCR)
        // snap: 文字取得させたいSoftwareBitmap
        async Task<OcrResult> RunWin10Ocr(SoftwareBitmap snap)
        {
            // OCRの準備。言語設定を英語にする
            //Windows.Globalization.Language language = new Windows.Globalization.Language("en");
            OcrEngine ocrEngine = OcrEngine.TryCreateFromUserProfileLanguages();

            // OCRをはしらせる
            var ocrResult = await ocrEngine.RecognizeAsync(snap);
            return ocrResult;
        }

        // SoftwareBitmapのスクリーンキャプチャ取得
        public async Task<SoftwareBitmap> GetSoftwareSnapShot()
        {
            // 画面のスクリーンキャプチャをBitmapとして取得(このメソッドは上の記事で作成したもの)
            Bitmap snap = new Bitmap(@"C:\test\1.bmp");

            // 上で取得したキャプチャ画像をファイルとして保存
            var folder = Directory.GetCurrentDirectory();
            var image_name = "ScreenCapture.png";
            StorageFolder appFolder = await StorageFolder.GetFolderFromPathAsync(@folder);
            snap.Save(folder + "\\" + image_name, System.Drawing.Imaging.ImageFormat.Png);
            SoftwareBitmap softwareBitmap;
            var bmpFile = await appFolder.GetFileAsync(image_name);

            // 保存した画像をSoftwareBitmap形式で読み込み
            using (IRandomAccessStream stream = await bmpFile.OpenAsync(FileAccessMode.Read))
            {
                BitmapDecoder decoder = await BitmapDecoder.CreateAsync(stream);
                softwareBitmap = await decoder.GetSoftwareBitmapAsync();
            }

            // 保存した画像ファイルの削除
            File.Delete(folder + "\\" + image_name);

            // SoftwareBitmap形式の画像を返す
            return softwareBitmap;
        }

        

        

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        

        private void groupBox1_Enter(object sender, EventArgs e)
        {
            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {

        }

        

        private void toolStripContainer1_LeftToolStripPanel_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            if (_image != null)
            {
                //label1にログを残す
                label1.ForeColor = Color.LawnGreen;
                label1.Text = "+ Now Loading";
                var textToWrite = richTextBox1.Text;
                int intVal1 = Decimal.ToInt32(numericUpDown1.Value);
                int intVal2 = Decimal.ToInt32(numericUpDown2.Value);
                uint uintVal4 = Decimal.ToUInt32(numericUpDown4.Value);
                uint uintVal5 = Decimal.ToUInt32(numericUpDown5.Value);

                // These settings will create a new caption
                // which automatically resizes the text to best
                // fit within the box.

                var settings = new MagickReadSettings()
                {
                    Font = @"C:\Windows\Fonts\BIZ-UDGothicR.ttc",
                    FillColor = new MagickColor(color0.R, color0.G, color0.B),
                    TextGravity = Gravity.Center,
                    BackgroundColor = MagickColors.Transparent,
                    Width = uintVal4, // width of text box
                    Height = uintVal5 // height of text box
                };

                using (var caption = new MagickImage($"caption:{textToWrite}", settings))
                {

                    // Add the caption layer on top of the background image
                    // at position 590,450
                    _image.Composite(caption, intVal1, intVal2, CompositeOperator.Over);
                    _image.Write(@"C:\test\1.bmp");
                }


                //PictureBox1に表示する
                pictureBox1.ImageLocation = @"C:\test\1.bmp";
                //label1にログを残す
                label1.ForeColor = Color.LawnGreen;
                label1.Text = "+ Add Text";
            }
            else
            {
                //label1にログを残す
                label1.ForeColor = Color.Red;
                label1.Text = "- No Image!";
            }

        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            ColorDialog MyDialog = new ColorDialog();
            // Keeps the user from selecting a custom color.
            MyDialog.AllowFullOpen = false;
            // Allows the user to get help. (The default is false.)
            MyDialog.ShowHelp = true;
            // Sets the initial color select to the current text color.
            MyDialog.Color = color0;

            // Update the text box color if the user clicks OK 
            if (MyDialog.ShowDialog() == DialogResult.OK)
            {
                color0 = MyDialog.Color;
                panel1.BackColor = color0;
                //label1にログを残す
                label1.ForeColor = Color.LawnGreen;
                label1.Text = "+ Color Setted";
            }
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            if (_image != null)
            {
                int x = Decimal.ToInt32(numericUpDown1.Value);
                int y = Decimal.ToInt32(numericUpDown2.Value);
                int width = Decimal.ToInt32(numericUpDown4.Value);
                int height = Decimal.ToInt32(numericUpDown5.Value);

                // 塗りつぶしの長方形を描画
                var drawables = new Drawables()
                    .FillColor(new MagickColor(color0.R, color0.G, color0.B)) // 塗りつぶし色を黒に設定
                    .Rectangle(x, y, x + width, y + height); // (10, 20)から(110, 100)の長方形

                // 描画
                drawables.Draw(_image);

                _image.Write(@"C:\test\1.bmp");
                //PictureBox1に表示する
                pictureBox1.ImageLocation = @"C:\test\1.bmp";
                //label1にログを残す
                label1.ForeColor = Color.LawnGreen;
                label1.Text = "+ Paint Out";
            }
            else
            {
                //label1にログを残す
                label1.ForeColor = Color.Red;
                label1.Text = "- No Image!";
            }
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            if (_image != null)
            {
                int x = Decimal.ToInt32(numericUpDown1.Value);
                int y = Decimal.ToInt32(numericUpDown2.Value);
                uint width = Decimal.ToUInt32(numericUpDown4.Value);
                uint height = Decimal.ToUInt32(numericUpDown5.Value);

                using (var croppedImage = _image.Clone(new MagickGeometry(x, y, width, height)))
                {
                    // 切り取った画像を保存
                    croppedImage.Write(@"C:\test\1.bmp");
                }
                //再読み込み
                _image = new MagickImage(@"C:\test\1.bmp");
                //PictureBox1に表示する
                pictureBox1.ImageLocation = @"C:\test\1.bmp";
                //label1にログを残す
                label1.ForeColor = Color.LawnGreen;
                label1.Text = "+ Image Cutted";
            }
            else
            {
                //label1にログを残す
                label1.ForeColor = Color.Red;
                label1.Text = "- No Image!";
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (_image != null)
            {
                // サイズ変更の例（幅と高さを125%に）
                _image.Resize(new MagickGeometry(_image.Width * 5 / 4, _image.Height * 5 / 4));
                //pictureBox1.Image = _image.ToBitmap();
                //BMP形式で保存する
                _image.Write(@"C:\test\1.bmp");
                //PictureBox1に表示する
                pictureBox1.ImageLocation = @"C:\test\1.bmp";
                //label1にログを残す
                label1.ForeColor = Color.LawnGreen;
                label1.Text = "+ Resized Larger";
            }
            else
            {
                //label1にログを残す
                label1.ForeColor = Color.Red;
                label1.Text = "- No Image!";
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            _image.Rotate(90); // 90度回転
            //pictureBox1.Image = _image.ToBitmap();
            //BMP形式で保存する
            _image.Write(@"C:\test\1.bmp");
            pictureBox1.ImageLocation = @"C:\test\1.bmp";
            //label1にログを残す
            label1.ForeColor = Color.LawnGreen;
            label1.Text = "+ Image Rotated";
        }

        private void recognizeToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void rotateToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            _image.Rotate(90); // 90度回転
            //pictureBox1.Image = _image.ToBitmap();
            //BMP形式で保存する
            _image.Write(@"C:\test\1.bmp");
            pictureBox1.ImageLocation = @"C:\test\1.bmp";
            //label1にログを残す
            label1.ForeColor = Color.LawnGreen;
            label1.Text = "+ Image Rotated";
        }

        private void largerToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (_image != null)
            {
                // サイズ変更の例（幅と高さを125%に）
                _image.Resize(new MagickGeometry(_image.Width * 5 / 4, _image.Height * 5 / 4));
                //pictureBox1.Image = _image.ToBitmap();
                //BMP形式で保存する
                _image.Write(@"C:\test\1.bmp");
                //PictureBox1に表示する
                pictureBox1.ImageLocation = @"C:\test\1.bmp";
                //label1にログを残す
                label1.ForeColor = Color.LawnGreen;
                label1.Text = "+ Resized Larger";
            }
            else
            {
                //label1にログを残す
                label1.ForeColor = Color.Red;
                label1.Text = "- No Image!";
            }
        }

        private void smallerToolStripMenuItem1_Click_1(object sender, EventArgs e)
        {
            if (_image != null)
            {
                // サイズ変更の例（幅と高さを75%に）
                _image.Resize(new MagickGeometry(_image.Width * 3 / 4, _image.Height * 3 / 4));
                //pictureBox1.Image = _image.ToBitmap();
                //BMP形式で保存する
                _image.Write(@"C:\test\1.bmp");
                //PictureBox1に表示する
                pictureBox1.ImageLocation = @"C:\test\1.bmp";
                //label1にログを残す
                label1.ForeColor = Color.LawnGreen;
                label1.Text = "+ Resized Smaller";
            }
            else
            {
                //label1にログを残す
                label1.ForeColor = Color.Red;
                label1.Text = "- No Image!";
            }
        }

        private void closedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void saveToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (_image != null)
            {
                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        _image.Write(saveFileDialog.FileName);
                        //label1にログを残す
                        label1.ForeColor = Color.LawnGreen;
                        label1.Text = "+ Image Saved";
                    }
                }
            }
            else
            {
                //label1にログを残す
                label1.ForeColor = Color.Red;
                label1.Text = "- No Image!";
            }
        }

        private void deleteImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_image != null)
            {
                // 保存した画像ファイルの削除
                //File.Delete(@"C:\test\1.bmp");
                //後始末
                _image = null;
                //終了
                //this.Close();
                //PictureBox1から削除する
                pictureBox1.Image = null;
                //label1にログを残す
                label1.ForeColor = Color.LawnGreen;
                label1.Text = "+ Image Deleted";
            }
            else
            {
                //label1にログを残す
                label1.ForeColor = Color.Red;
                label1.Text = "- No Image!";
            }
        }

        private void resetValueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = "";
            numericUpDown1.Value = 0;
            numericUpDown2.Value = 0;
            numericUpDown3.Value = 31;
            numericUpDown4.Value = 100;
            numericUpDown5.Value = 100;
            color0 = Color.WhiteSmoke;
            panel1.BackColor = color0;
            //label1にログを残す
            label1.ForeColor = Color.LawnGreen;
            label1.Text = "+ Value Resetted";
        }

        private void importAnotherImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_image != null)
            {
                using (OpenFileDialog openFileDialog = new OpenFileDialog())
                {
                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        int x = Decimal.ToInt32(numericUpDown1.Value);
                        int y = Decimal.ToInt32(numericUpDown2.Value);
                        MagickImage another_image = new MagickImage(openFileDialog.FileName);
                        // 重ねる
                        _image.Composite(another_image, x, y, CompositeOperator.Over);
                        //BMP形式で保存する
                        _image.Write(@"C:\test\1.bmp");
                        //PictureBox1に表示する
                        pictureBox1.ImageLocation = @"C:\test\1.bmp";
                        //label1にログを残す
                        label1.ForeColor = Color.LawnGreen;
                        label1.Text = "+ Image Imported";
                    }
                }
            }
            else
            {
                //label1にログを残す
                label1.ForeColor = Color.Red;
                label1.Text = "- No Image!";
            }
        }

        private void lightToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void ajustBrightnessToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void ajustContrastToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_image != null)
            {
                int intVal3 = Decimal.ToInt32(numericUpDown3.Value);
                // 明るさを調整
                _image.BrightnessContrast(new Percentage(intVal3), new Percentage(0));
                // 画像の保存
                _image.Write(@"C:\test\1.bmp");
                //PictureBox1に表示する
                pictureBox1.ImageLocation = @"C:\test\1.bmp";
                //label1にログを残す
                label1.ForeColor = Color.LawnGreen;
                label1.Text = "+ Bightness Ajusted";
            }
            else
            {
                //label1にログを残す
                label1.ForeColor = Color.Red;
                label1.Text = "- No Image!";
            }
        }

        private void ajustContrastToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (_image != null)
            {
                int intVal3 = Decimal.ToInt32(numericUpDown3.Value);
                // 明るさを調整
                _image.BrightnessContrast(new Percentage(0), new Percentage(intVal3));
                // 画像の保存
                _image.Write(@"C:\test\1.bmp");
                //PictureBox1に表示する
                pictureBox1.ImageLocation = @"C:\test\1.bmp";
                //label1にログを残す
                label1.ForeColor = Color.LawnGreen;
                label1.Text = "+ Bightness Ajusted";
            }
            else
            {
                //label1にログを残す
                label1.ForeColor = Color.Red;
                label1.Text = "- No Image!";
            }
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            if (_image != null)
            {
                // サイズ変更の例（幅と高さを75%に）
                _image.Resize(new MagickGeometry(_image.Width * 3 / 4, _image.Height * 3 / 4));
                //pictureBox1.Image = _image.ToBitmap();
                //BMP形式で保存する
                _image.Write(@"C:\test\1.bmp");
                //PictureBox1に表示する
                pictureBox1.ImageLocation = @"C:\test\1.bmp";
                //label1にログを残す
                label1.ForeColor = Color.LawnGreen;
                label1.Text = "+ Resized Smaller";
            }
            else
            {
                //label1にログを残す
                label1.ForeColor = Color.Red;
                label1.Text = "- No Image!";
            }
        }

        private void gradationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_image != null)
            {
                // エンボス効果を適用
                int intVal3 = Decimal.ToInt32(numericUpDown3.Value);
                _image.Emboss(2, intVal3); // ストレングスと半径の設定
                // 画像の保存
                _image.Write(@"C:\test\1.bmp");
                //PictureBox1に表示する
                pictureBox1.ImageLocation = @"C:\test\1.bmp";
                //label1にログを残す
                label1.ForeColor = Color.LawnGreen;
                label1.Text = "+ Emboss Applied";
            }
            else
            {
                //label1にログを残す
                label1.ForeColor = Color.Red;
                label1.Text = "- No Image!";
            }
        }

        private void mosaicToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }
    }
}
