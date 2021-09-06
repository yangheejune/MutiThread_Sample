using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


namespace PerformanceTestTools.MVVM.View
{
    public class user
    {
        public string account;
        public string password;
    }

    class UserInfo
    {
        private static int IV_LENGTH = 16;
        private readonly static char[] key = { 'J', 'i', 'r', 'a', 'n', 'S', 'e', 'c', 'u', 'r', 'i', 't', 'y', '!', 'N', 'e', 'w', 'T', 'e', 'c', 'h', '@', 'M', 'a', 's', 't', 'e', 'r', 'K', 'e', 'y', '#' };

        private string _userID;
        public string userID
        {
            get { return this._userID; }
            set { this._userID = value; }
        }
        private string _userPW;
        public string userPW
        {
            get { return this._userPW; }
            set { this._userPW = value; }
        }

        public async void Login()
        {

            string temp = GenerateHMAC(userPW);
            // ID, PW json에 넣기
             var obj = new user
            {
                account = userID,
                password = temp
             };

            string userInfoJson = JsonConvert.SerializeObject(obj);
            userInfoJson.Trim();

            string encrypt_userInfoJson = AES_Encrypt(userInfoJson);
            string urlencode_encrypt_userInfoJson = HttpUtility.UrlEncode(encrypt_userInfoJson);

            // 로그인 요청
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            
            HttpClient client = new HttpClient(clientHandler);
            
            string url = "https://10.52.10.70/api/s1/login/";
            var response = await client.PostAsync(url, new StringContent(urlencode_encrypt_userInfoJson, Encoding.UTF8, "application/x-www-form-urlencoded"));
            //var responseContent = response.Content.ReadAsStringAsync();
            if (response.StatusCode.ToString().Contains("OK"))
            {
                // 삭제

            }
            else
            {

            }
            // 데이터 복호화가 안됨
            //var responseContent = await response.RequestMessage.Content.ReadAsStringAsync();
            string urlDecodeResponseContent = HttpUtility.UrlDecode(urlencode_encrypt_userInfoJson);
            
            byte[] byteResponseContent = Convert.FromBase64String(urlDecodeResponseContent);

            // 데이터 복호화 해서 완료되었는지 확인
            string Decrypt_responseContent = AES_Decrypt(Encoding.UTF8.GetString(byteResponseContent));
            //string Decrypt_responseContent = AES_Decrypt(Base64Decode(urlDecodeResponseContent));
            // 정책 업데이트까지 완료 하면 로그인 성능 테스트 끝 UpdateDisarmPolicy();

            Console.WriteLine("Login id :" + userID + "pw : " + userPW );
            MessageBox.Show("로그인 정보 : ID : " + userID + ", 암호 : " + userPW, "로그인", MessageBoxButton.OK);
        }

 
        // HMAC 생성 함수
        private string GenerateHMAC(string password)
        {
            // 키 생성
            //var hmac_key = Encoding.UTF8.GetBytes(key);
            var hmac_key = new byte[] { 0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x0a, 0x0b, 0x0c, 0x0d, 0x0e, 0x0f, 0x10, 0x11, 0x12, 0x13, 0x14, 0x15, 0x16, 0x17, 0x18, 0x19, 0x1a, 0x1b, 0x1c, 0x1d, 0x1e, 0x1f };

            var message = Encoding.UTF8.GetBytes(password);

            // HMAC-SHA256 객체 생성
            using (HMACSHA256 sha = new HMACSHA256(hmac_key))
            {
                // 암호화
                var hash = sha.ComputeHash(message);

                // base64 컨버팅
                return Convert.ToBase64String(hash);
            }
        }

        private string Base64Encode(string data)
        {
            try
            {
                byte[] encData_byte = new byte[data.Length];
                encData_byte = System.Text.Encoding.UTF8.GetBytes(data);
                string encodedData = Convert.ToBase64String(encData_byte);
                return encodedData;
            }
            catch (Exception e)
            {
                throw new Exception("Error in Base64Encode: " + e.Message);
            }
        }

        private string Base64Decode(string data)
        {
            try
            {
                System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
                System.Text.Decoder utf8Decode = encoder.GetDecoder();

                byte[] todecode_byte = Convert.FromBase64String(data);
                int charCount = utf8Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
                char[] decoded_char = new char[charCount];
                utf8Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
                string result = new string(decoded_char);
                return result;
            }
            catch (Exception e)
            {
                throw new Exception("Error in Base64Decode: " + e.Message);
            }
        }

        // IV 값 랜덤하게 획득
        private static byte[] generateIV()
        {
            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                byte[] nonce = new byte[IV_LENGTH];
                rng.GetBytes(nonce);
                return nonce;
            }
        }

        // 암호화
        private string AES_Encrypt(string sourceString)
        {
            using (RijndaelManaged aes = new RijndaelManaged())
            {
                // aes256
                aes.BlockSize = 128;
                aes.KeySize = 256;
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;
                aes.Key = Encoding.UTF8.GetBytes(key);
                aes.IV = generateIV();
                // aes256 암호화 설정
                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                // 암호화 진행
                byte[] encrypted;
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(sourceString);
                        }
                        //byte[] inputBytes = Encoding.UTF8.GetBytes(sourceString);
                        //csEncrypt.Write(inputBytes, 0, inputBytes.Length);
                    }
                    encrypted = msEncrypt.ToArray();
                }

                // IV 값 앞에 16바이트 추가
                byte[] ivValue = aes.IV;
                byte[] sumBytes = new byte[ivValue.Length + encrypted.Length];
                Array.Copy(ivValue, 0, sumBytes, 0, ivValue.Length);
                Array.Copy(encrypted, 0, sumBytes, ivValue.Length, encrypted.Length);

                // Base64 인코딩
                return Convert.ToBase64String(sumBytes);
            }
        }

        private string AES_Decrypt(string sourceString)
        {
            byte[] decrypted;

            try
            {
                using (RijndaelManaged aes = new RijndaelManaged())
                {
                    // aes256 설정
                    aes.BlockSize = 128;
                    aes.KeySize = 256;
                    aes.Mode = CipherMode.CBC;
                    aes.Padding = PaddingMode.PKCS7;
                    aes.Key = Encoding.UTF8.GetBytes(key);
                    ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                    // 스크링을 byte로 변경 후
                    // IV 값 추출을 위해 앞 16바이트 추출 후 임시 temp.aes 파일 만듬
                    byte[] decodedByte = Encoding.Default.GetBytes(sourceString);
                    byte[] ivValue = new byte[IV_LENGTH];
                    byte[] originalFile = new byte[decodedByte.Length - IV_LENGTH];
                    Array.Copy(decodedByte, 0, ivValue, 0, IV_LENGTH);
                    Array.Copy(decodedByte, IV_LENGTH, originalFile, 0, decodedByte.Length - IV_LENGTH);

                    using (MemoryStream ms = new MemoryStream())
                    {
                        ms.Write(originalFile, 0, originalFile.Length);

                        // IV 값 설정
                        aes.IV = ivValue;

                        // aes256 복호화 설정
                        var decrypt = aes.CreateDecryptor();

                        // 복호화 진행

                        using (MemoryStream msDecrypt = new MemoryStream())
                        {
                            using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decrypt, CryptoStreamMode.Write))
                            {
                               // byte[] inputBytes = ms.ToArray();
                                //int paddingSizeIndex = inputBytes.Length;
                                //char charpaddingsSize = inputBytes[paddingSizeIndex - 1];
                                //string a = paddingsSize.ToString();
                                csDecrypt.Write(originalFile, 0, originalFile.Length);
                            }
                            decrypted = msDecrypt.ToArray();
                        }
                    }
                }
                return Encoding.Default.GetString(decrypted);
            }
            catch (OverflowException)
            {
                return sourceString;
            }

        }


    }
    /// <summary>
    /// HomeView.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class LoginPerformanceView : UserControl
    {
        public LoginPerformanceView()
        {
            InitializeComponent();
        }

        private void ThreadCount_GotFocus(object sender, RoutedEventArgs e)
        {

        }

        private void ThreadCount_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void ThreadCount_TextChanged(object sender, TextChangedEventArgs e)
        {
            int ipno = 1;
            try
            {
                ipno = Convert.ToInt32(ThreadCount.Text);

                if (ipno > 2000)
                {
                    /// usercontrol은 부모 창에 패널로 붙어 있기 때문에 자신의 x,y 좌표를 가지고 있지 않다. 그렇기 때문에 화면의 중앙에 띄울수 있도록 함.
                    MessageBox.Show("스레드 개수는 최대 2000개 이하여야 합니다.", "Thread Error", MessageBoxButton.OK);
                    ThreadCount.Text = "2000";
                }
                else if (ipno < 1)
                {
                    MessageBox.Show("스레드 개수는 최소 1개 이어야 합니다.", "Thread Error", MessageBoxButton.OK);
                    ThreadCount.Text = "1";
                }
            }
            catch (FormatException)
            {
                ipno = 1;
                ThreadCount.Text = "";
            }
        }

        private void UpCount_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DownCount_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ThreadStart_Click(object sender, RoutedEventArgs e)
        {

        }

        private void startBtn_Click(object sender, RoutedEventArgs e)
        {
            int threadcount = Int32.Parse(ThreadCount.Text);
            UserInfo userinfo = new UserInfo();
            userinfo.userID = UserID.Text;
            userinfo.userPW = UserPW.Password;
           

            // 스레드 갯수 만큼 스레드 제작
            for (int i = 0; i < threadcount; i++)
            {
                new Thread(new ThreadStart(userinfo.Login)).Start();
            }

            // 스레드 하나 하나 마다 통신 모듈 붙이기 dll을 붙여야 할거같은데...
            //int nresult = (int)common.NativeMethods.sdk_SetLogin(struserid, struserps);
        }

        static void Login()
        {
            Console.WriteLine("Login");
        }

        private void Logview_Click(object sender, RoutedEventArgs e)
        {
            string filePath = Directory.GetCurrentDirectory()+ "\\Log";
            Process.Start("explorer.exe", filePath);
        }
    }
}
