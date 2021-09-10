using log4net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PerformanceTestTools.MVVM.ViewModel;
using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
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

    /// <summary>
    /// HomeView.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class LoginPerformanceView : UserControl
    {
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        LoginPerformanceViewModel loginPerformaceViewModel = new LoginPerformanceViewModel();

        static Object monitorLock = new System.Object();
        private static int IV_LENGTH = 16;
        private readonly static char[] key = { 'J', 'i', 'r', 'a', 'n', 'S', 'e', 'c', 'u', 'r', 'i', 't', 'y', '!', 'N', 'e', 'w', 'T', 'e', 'c', 'h', '@', 'M', 'a', 's', 't', 'e', 'r', 'K', 'e', 'y', '#' };

        public LoginPerformanceView()
        {
            this.DataContext = loginPerformaceViewModel;

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

            string[] param = { UserID.Text, UserPW.Password };
            
            // 스레드 갯수 만큼 스레드 제작
            for (int i = 0; i < threadcount; i++)
            {
                Thread th = new Thread(new ParameterizedThreadStart(LoginAsync));
                th.Start(param);

            }

            // 스레드 하나 하나 마다 통신 모듈 붙이기 dll을 붙여야 할거같은데...
            //int nresult = (int)common.NativeMethods.sdk_SetLogin(struserid, struserps);
        }

        private async void LoginAsync(object param)
        {
            Thread cur_thread_id = Thread.CurrentThread;
            log.Info("Thread(" + cur_thread_id.ManagedThreadId.ToString() + ") Start");
            string token;
            string result = null;
            JObject json;
            string[] data = param as string[];
            string temp = GenerateHMAC(data[1]);
            // ID, PW json에 넣기
            var obj = new user
            {
                account = data[0],
                password = temp
            };

            string userInfoJson = JsonConvert.SerializeObject(obj);

            //string aes_test = AES_TEST(userInfoJson);

            string encrypt_userInfoJson = AES_Encrypt(userInfoJson);
            string urlencode_encrypt_userInfoJson = HttpUtility.UrlEncode(encrypt_userInfoJson);

            // 로그인 요청
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

            HttpClient client = new HttpClient(clientHandler);

            string loginUrl = "https://10.52.10.70/api/s1/login/";
            string policyUrl = "https://10.52.10.70/api/s1/get_policy/";
            var loginresponse = await client.PostAsync(loginUrl, new StringContent(urlencode_encrypt_userInfoJson, Encoding.UTF8, "application/x-www-form-urlencoded"));
            var responseContent = loginresponse.Content.ReadAsStringAsync().Result;

            // 데이터 복호화 해서 완료되었는지 확인
            // 복호화 전에 string에 <HTML> 있으면 fail
            if (responseContent.IndexOf("html") == -1)
            {
                string Decrypt_responseContent = AES_Decrypt(responseContent.Trim('\"'));
                json = JObject.Parse(Decrypt_responseContent.Trim('\"'));
                result = json["success"].ToString();

                // 로그인 성공시 해당 토큰 획득하여 정책정보 받아오고 완료
                if (result.CompareTo("True") == 0)
                {
                    token = json["token"].ToString();
                    JObject user = new JObject(new JProperty("account", data[0]));
                    string account = JsonConvert.SerializeObject(obj);
                    string encrypt_account = AES_Encrypt(account);
                    string urlencode_encrypt_account = HttpUtility.UrlEncode(encrypt_account);

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token);

                    var policyresponse = await client.PostAsync(policyUrl, new StringContent(urlencode_encrypt_account, Encoding.UTF8, "application/x-www-form-urlencoded"));
                    var PolicyresponseContent = policyresponse.Content.ReadAsStringAsync().Result;
                    if (PolicyresponseContent.IndexOf("html") == -1)
                    {
                        // 데이터 복호화 해서 완료되었는지 확인
                        string Decrypt_PolicyresponseContent = AES_Decrypt(PolicyresponseContent.Trim('\"'));
                        JObject PolicyJson = JObject.Parse(Decrypt_PolicyresponseContent);
                        string PolicyResult = PolicyJson["success"].ToString();
                        if (PolicyResult.CompareTo("True") == 0)
                        {
                            Monitor.Enter(monitorLock);
                            try
                            {
                                loginPerformaceViewModel.SuccessCount++;
                            }
                            finally
                            {
                                Monitor.Exit(monitorLock);
                            }
                        }
                        else  // 정책 전송 실패
                        {
                            Monitor.Enter(monitorLock);
                            try
                            {
                                loginPerformaceViewModel.FailCount++;
                            }
                            finally
                            {
                                Monitor.Exit(monitorLock);
                            }
                        }
                    }
                    else  // 서버 응답 실패
                    {
                        Monitor.Enter(monitorLock);
                        try
                        {
                            loginPerformaceViewModel.FailCount++;
                        }
                        finally
                        {
                            Monitor.Exit(monitorLock);
                        }
                    }

                }
                else // 로그인 실패
                {
                    Monitor.Enter(monitorLock);
                    try
                    {
                        loginPerformaceViewModel.FailCount++;
                    }
                    finally
                    {
                        Monitor.Exit(monitorLock);
                    }
                }
            }
            else
            {
                Monitor.Enter(monitorLock);
                try
                {
                    loginPerformaceViewModel.FailCount++;
                }
                finally
                {
                    Monitor.Exit(monitorLock);
                }
            }
            log.Info("Thread(" + cur_thread_id.ManagedThreadId.ToString() + ") End");
        }
    

        private void Logview_Click(object sender, RoutedEventArgs e)
        {
            string filePath = Directory.GetCurrentDirectory()+ "\\Log";
            Process.Start("explorer.exe", filePath);
        }

        // HMAC 생성 함수
        private static string GenerateHMAC(string password)
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
                byte[] encrypted = null;
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        byte[] inputBytes = Encoding.UTF8.GetBytes(sourceString);
                        csEncrypt.Write(inputBytes, 0, inputBytes.Length);
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

        public string AES_TEST(string sourceString)
        {
            string temp = null;
            byte[] decrypted = null;
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
                byte[] encrypted = null;
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        byte[] inputBytes = Encoding.UTF8.GetBytes(sourceString);
                        csEncrypt.Write(inputBytes, 0, inputBytes.Length);
                    }
                    encrypted = msEncrypt.ToArray();
                }

                // IV 값 앞에 16바이트 추가
                byte[] ivValueEn = aes.IV;
                byte[] sumBytes = new byte[ivValueEn.Length + encrypted.Length];
                Array.Copy(ivValueEn, 0, sumBytes, 0, ivValueEn.Length);
                Array.Copy(encrypted, 0, sumBytes, ivValueEn.Length, encrypted.Length);

                // Base64 인코딩
                temp = Convert.ToBase64String(sumBytes);

                // url 인코딩
                string urlEncode = HttpUtility.UrlEncode(temp);
                // url 디코딩
                string urlDecode = HttpUtility.UrlDecode(urlEncode);

                // 스크링을 byte로 변경 후
                // IV 값 추출을 위해 앞 16바이트 추출 후 임시 temp.aes 파일 만듬
                byte[] decodedByte = Convert.FromBase64String(urlDecode);
                byte[] ivValueDe = new byte[IV_LENGTH];
                byte[] originalFile = new byte[decodedByte.Length - IV_LENGTH];
                Array.Copy(decodedByte, 0, ivValueDe, 0, IV_LENGTH);
                Array.Copy(decodedByte, IV_LENGTH, originalFile, 0, decodedByte.Length - IV_LENGTH);

                using (MemoryStream ms = new MemoryStream())
                {
                    ms.Write(originalFile, 0, originalFile.Length);

                    // IV 값 설정
                    aes.IV = ivValueDe;

                    // aes256 복호화 설정
                    var decrypt = aes.CreateDecryptor();

                    // 복호화 진행

                    using (MemoryStream msDecrypt = new MemoryStream())
                    {
                        using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decrypt, CryptoStreamMode.Write))
                        {
                            byte[] inputBytes = ms.ToArray();
                            //int paddingSizeIndex = inputBytes.Length;
                            //char charpaddingsSize = inputBytes[paddingSizeIndex - 1];
                            //string a = paddingsSize.ToString();
                            csDecrypt.Write(inputBytes, 0, inputBytes.Length);
                        }
                        decrypted = msDecrypt.ToArray();
                    }
                }
            }
            return Encoding.UTF8.GetString(decrypted);
        }

        private string AES_Decrypt(string sourceString)
        {
            try
            {
                byte[] decrypted = null;

                using (RijndaelManaged aes = new RijndaelManaged())
                {
                    // aes256 설정
                    aes.BlockSize = 128;
                    aes.KeySize = 256;
                    aes.Mode = CipherMode.CBC;
                    aes.Padding = PaddingMode.PKCS7;
                    aes.Key = Encoding.UTF8.GetBytes(key);

                    // 스크링을 byte로 변경 후
                    // IV 값 추출을 위해 앞 16바이트 추출 후 임시 temp.aes 파일 만듬
                    byte[] decodedByte = Convert.FromBase64String(sourceString);
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
                                byte[] inputBytes = ms.ToArray();
                                //int paddingSizeIndex = inputBytes.Length;
                                //char charpaddingsSize = inputBytes[paddingSizeIndex - 1];
                                //string a = paddingsSize.ToString();
                                csDecrypt.Write(inputBytes, 0, inputBytes.Length);
                            }
                            decrypted = msDecrypt.ToArray();
                        }
                    }
                }
                return Encoding.UTF8.GetString(decrypted);
            }
            catch (OverflowException)
            {
                return sourceString;
            }

        }
    }
}
