using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using GTA;
using GTA.UI;

namespace Anti46EEK572
{
    public class Class1 : Script
    {
        XElement xml = null;
        bool operatable = true; //スクリプトが動作可能であるか
        static bool helped = false;
        bool player_vehicle = false; //プレイヤー車両のナンバープレート変更
        bool npc_vehicles = false; //NPC車両のナンバープレート変更

        //バージョン情報
        private static AssemblyName assembly = Assembly.GetExecutingAssembly().GetName(); //アセンブリ情報
        private string ver = assembly.Version.ToString(3); // 形式は0.0.0

        string ScriptName = "Anti46EEK572";

        public Class1()
        {

            Tick += OnTick;

            // XMLファイルがあるか
            if (!File.Exists(@"scripts\Anti46EEK572.xml"))
            {
                NotificationIcon icon = NotificationIcon.Blocked; //禁止アイコン
                Notification.Show(icon, $"{ScriptName} - v{ver}", "~r~Error", not_found_xml_message(Game.Language)); 
                operatable = false;
                return;
            }

            //xmlファイルを指定する
            xml = XElement.Load(@"scripts\Anti46EEK572.xml");

            ScriptSettings ini = ScriptSettings.Load(@"scripts\Anti46EEK572.ini"); //INI File
            // iniのデータを読み込む (セクション、キー、デフォルト値)
            player_vehicle = ini.GetValue<bool>("Settings", "PlayerVehicle", false);
            npc_vehicles = ini.GetValue<bool>("Settings", "NPCVehicles", false);

            operatable = xml_check();

            if(!operatable) //スクリプト実行不可の場合
            {
                NotificationIcon icon = NotificationIcon.Blocked; //禁止アイコン
                Notification.Show(icon, $"{ScriptName} - v{ver}", "~r~Error", not_operatable_message(Game.Language));
                Task t = help_message_thread(); // 非同期処理
            }
#if DEBUG
            Notification.Show("Anti46EEK572 is READY!");
#endif
        }

        private void OnTick(object sender, EventArgs e)
        {
            // 実行可能であるか
            if(operatable)
            {
                // プレイヤー車両に対してスクリプトを動作させるか
                if(player_vehicle)
                {

                    // 車両に乗車しているか
                    if (Game.Player.Character.CurrentVehicle != null)
                    {
                        plate_change(Game.Player.Character.CurrentVehicle);
                    }
                }

                // NPC車両に対してスクリプトを動作させるか
                if (npc_vehicles)
                {
                    // ワールドに存在する全ての車両を取得
                    Vehicle[] world_vehicles = World.GetAllVehicles();

                    // 1つずつ処理
                    foreach (Vehicle v in world_vehicles)
                    {
                        plate_change(v);
                    }
                }
            }
            else //実行不可能な場合
            {
                if (!helped) // フラグが立たない間はヘルプメッセージを表示させる。（フラグは非同期処理で20秒後に立つ）
                {
                    Screen.ShowHelpTextThisFrame(not_operatable_help(Game.Language), true);
                }
            }

        }

        /// <summary>
        /// ヘルプメッセージを表示させるためのフラグメソッド
        /// </summary>
        static async Task help_message_thread()
        {
            await Task.Delay(20000); // 20s wait
            helped = true; // 20秒後、フラグをTrueにし、ヘルプメッセージを非表示にさせる。
        }

        /// <summary>
        /// XMLファイルが見つからなかった場合のエラーメッセージを返します。
        /// </summary>
        /// <param name="lang">ゲームの言語</param>
        /// <returns>エラーメッセージ</returns>
        private string not_found_xml_message(Language lang)
        {
            string return_message = "";

            if (lang == Language.Japanese) //日本語GTAVの場合
            {
                return_message = $"~r~{ScriptName}.xml が見つからないため、スクリプトの動作を停止します。~n~~o~XMLファイルが本体ファイルと同じディレクトリ階層に存在するか確認し、スクリプトを再読込して下さい。";
            }
            else
            {
                return_message = $"~r~Script will stop working because {ScriptName}.xml cannot be found. ~n~~o~Please make sure that the XML file exists in the same directory hierarchy as the script file and reload the script.";
            }

            return return_message;
        }

        /// <summary>
        ///スクリプトを実行できない場合のエラーメッセージを返します。
        /// </summary>
        /// <param name="lang">ゲームの言語</param>
        /// <returns>エラーメッセージ</returns>
        private string not_operatable_message(Language lang)
        {
            string return_message = "";

            if (lang == Language.Japanese) //日本語GTAVの場合
            {
                return_message = $"~r~{ScriptName}.xml の記述が正しくないため、スクリプトの動作を停止します。~n~~o~XMLファイルの内容を確認・修正し、スクリプトを再読込してください。";
            }
            else
            {
                return_message = $"~r~The script will stop working because {ScriptName}.xml is incorrect. ~n~~o~Please check and correct the contents of the XML file, then reload the script.";
            }

            return return_message;
        }

        /// <summary>
        ///スクリプトを実行できない場合のエラーメッセージを返します。
        /// </summary>
        /// <param name="lang">ゲームの言語</param>
        /// <returns>エラーメッセージ</returns>
        private string not_operatable_help(Language lang)
        {
            string return_message = "";

            if (lang == Language.Japanese) //日本語GTAVの場合
            {
                return_message = $"{ScriptName}.xml の~o~Plateタグに指定したナンバープレート~s~は~y~Overrideタグ~s~に~r~指定できない。~n~~s~XMLファイルを確認してみよう。";
            }
            else
            {
                return_message = $"~o~The license plate specified in the Plate tag~s~ in {ScriptName}.xml ~r~cannot be specified~s~ in the ~y~Override tag.~s~ ~n~Let's check the XML file.";
            }

            return return_message;
        }



        /// <summary>
        /// XMLが正しく記述されているかを確認します。
        /// </summary>
        /// <returns>スクリプトが動作可能であるか</returns>
        private bool xml_check()
        {
            // 比較用リストの用意
            List<string> plates = new List<string>();
            List<string> overrides = new List<string>();

            // Plateタグ全てを取得
            IEnumerable<XElement> pls = xml.Descendants("Plate");
            foreach (XElement pl in pls)
            {
                plates.Add(pl.Value.Trim()); //前後の空白を削除して追加
            }

            // Overrideタグ全てを取得
            IEnumerable<XElement> ovrs = xml.Descendants("Override");
            foreach (XElement ovr in ovrs)
            {
                overrides.Add(ovr.Value.Trim()); //前後の空白を削除して追加
            }

            foreach (string p in plates)
            {
                if (overrides.Contains(p)) //OverrideタグにPlateタグに指定されているナンバープレートが含まれている場合
                {
                    return false; //スクリプト動作不可
                }
            }

            return true;
        }

        /// <summary>
        /// ナンバープレートを変更します。
        /// </summary>
        /// <param name="plate">変更対象の車両。</param>
        private void plate_change(Vehicle vehicle)
        {

            //Plateタグ内の情報を取得する
            IEnumerable<XElement> infos = from item in xml.Elements("PlateSetup").Elements("Plates").Elements("Plate")
                                          select item;

            bool exist_custom_settings = false;
            //データ数分ループして、存在チェック
            foreach (XElement info in infos)
            {
                if (info.Value == vehicle.Mods.LicensePlate)
                {
                    exist_custom_settings = true;
                    break;
                }
            }

            // 指定があった場合
            if (exist_custom_settings)
            {

                // 指定されたPlateタグに「plate」が含まれているPlateSetupを検索
                var data = xml.Descendants("PlateSetup")
                                   .Where(ps => ps.Descendants("Plate").Any(p => p.Value == vehicle.Mods.LicensePlate))
                                   .Descendants("Override")
                                   .Select(o => o.Value)
                                   .ToList();

                //「plate」が指定されたものであるナンバーを取得する。(LEGACY)
                /*
                IEnumerable<XElement> data = from item in xml.Elements("PlateSetup")
                                           where item.Element("Plates").Element("Plate").Value == plate
                                           select item.Elements("Overrides").Elements("Override");
                */

                List<string> override_number_list = new List<string>();

                //データ数分ループして、追加
                foreach (string d in data)
                {
                    override_number_list.Add(d);
#if DEBUG
                        Notification.Show($"Anti46EEK572: Add -> {d}");
#endif
                }
                //string override_plate_number = data.ElementAt(0); //変数に格納

                string pn = "";

                if (override_number_list.Count == 0)
                {
                    pn = random_plate();
                }
                else
                {
                    Random random = new Random();
                    pn = override_number_list[random.Next(override_number_list.Count)];
                    if(pn == "") //指定もしているがランダム選出も指定している場合で、ランダムが選ばれた場合
                    {
                        pn = random_plate();
                    }
                }

                vehicle.Mods.LicensePlate = pn;
#if DEBUG
                Notification.Show($"Anti46EEK572: {plate} -> {pn}");
#endif
            }
        }

        /// <summary>
        /// ランダムにナンバープレートを生成します。
        /// </summary>
        /// <returns>生成されたナンバープレート。</returns>
        private string random_plate()
        {
            string alphabets = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string numbers = "0123456789";
            var char_array = new char[8];
            Random random = new Random();

            for (int i = 0; i < char_array.Length; i++)
            {
                if (i < 2 | i > 4)
                {
                    char_array[i] = numbers[random.Next(numbers.Length)]; //数字から
                }
                else
                {
                    char_array[i] = alphabets[random.Next(alphabets.Length)]; //英字から
                }
            }

            return new String(char_array);
        }
    }
}
