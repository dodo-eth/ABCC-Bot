using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Web;
using System.Net;
using Newtonsoft.Json;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InlineQueryResults;
using Telegram.Bot.Types.ReplyMarkups;
using System.Threading;
using System.IO;
using System.Diagnostics;

using System.Text.RegularExpressions;


namespace New_Bot_Abcc
{
    public partial class Form1 : Form
    {
        BackgroundWorker bw;

        public Form1()
        {

            ///CHAT ID -271196401



            InitializeComponent();
            this.bw = new BackgroundWorker();
            this.bw.DoWork += bw_DoWork;



        }

        async void bw_DoWork(object sender, DoWorkEventArgs e)
        {


            var worker = sender as BackgroundWorker;
            var key = e.Argument as String; // получаем ключ из аргументов
            try
            {
                var Bot = new Telegram.Bot.TelegramBotClient(key); // инициализируем API

                try
                {
                    await Bot.SetWebhookAsync(""); // !!!!!!!!!!!!!!!!!!!!!!ЦИКЛ ПЕРЕЗАПУСКА

                }
                catch
                {
                    await Bot.SetWebhookAsync("");
                }


                // Inlin'ы
                Bot.OnInlineQuery += async (object si, Telegram.Bot.Args.InlineQueryEventArgs ei) =>
                {

                    var query = ei.InlineQuery.Query;


                };

                Bot.OnCallbackQuery += async (object sc, Telegram.Bot.Args.CallbackQueryEventArgs ev) =>
                {
                    var message = ev.CallbackQuery.Message;
                    if (ev.CallbackQuery.Data == "CallDaily")
                    {
                        await Bot.DeleteMessageAsync(message.Chat.Id, message.MessageId);
                        await Bot.SendTextMessageAsync(message.Chat.Id, "CallDaily", ParseMode.Html, false, false, 0, null);
                    }
                    
                    if (ev.CallbackQuery.Data == "PutDaily")
                    {
                        await Bot.DeleteMessageAsync(message.Chat.Id, message.MessageId);
                        await Bot.SendTextMessageAsync(message.Chat.Id, "PutDaily", ParseMode.Html, false, false, 0, null);
                    }
                     
                    if (ev.CallbackQuery.Data == "Call6Hr")
                    {
                        await Bot.DeleteMessageAsync(message.Chat.Id, message.MessageId);
                        await Bot.SendTextMessageAsync(message.Chat.Id, "Call6Hr", ParseMode.Html, false, false, 0, null);
                    }
                     
                    if (ev.CallbackQuery.Data == "Put6Hr")
                    {
                        await Bot.DeleteMessageAsync(message.Chat.Id, message.MessageId);
                        await Bot.SendTextMessageAsync(message.Chat.Id, "Put6Hr", ParseMode.Html, false, false, 0, null);
                    }
                };

                Bot.OnUpdate += async (object su, Telegram.Bot.Args.UpdateEventArgs evu) =>
                {



                    try
                    {
                        var update = evu.Update;
                        var message = update.Message;

                        if (message == null) return;
                        if ( message.Type == Telegram.Bot.Types.Enums.MessageType.Text)
                        {

                            Data.VANGA = 87500 / 2;

                            if (message.Text == "/last_block@ProAggressive_bot")
                            {
                                try
                                {

                                    System.Net.WebClient wc = new System.Net.WebClient();
                                    String Response = wc.DownloadString("https://abcc.com/mining/computing_power.json");
                                    String btc1 = System.Text.RegularExpressions.Regex.Match(Response, @"""btc"":""[0-9]+.[0-9]+""", RegexOptions.RightToLeft).Groups[0].Value;
                                    String eth1 = System.Text.RegularExpressions.Regex.Match(Response, @"""eth"":""[0-9]+.[0-9]+""", RegexOptions.RightToLeft).Groups[0].Value;
                                    String usdt1 = System.Text.RegularExpressions.Regex.Match(Response, @"""usdt"":""[0-9]+.[0-9]+""", RegexOptions.RightToLeft).Groups[0].Value;

                                    String btc = System.Text.RegularExpressions.Regex.Match(btc1, @"[0-9]+\S+\d").Groups[0].Value;
                                    String eth = System.Text.RegularExpressions.Regex.Match(eth1, @"[0-9]+\S+\d").Groups[0].Value;
                                    String usdt = System.Text.RegularExpressions.Regex.Match(usdt1, @"[0-9]+\S+\d").Groups[0].Value;

                                    String btc_buy_and_sell_Response = wc.DownloadString("https://api.abcc.com/api/v1/exchange/order_book?market_code=btcusdt");
                                    String btc_sell1 = System.Text.RegularExpressions.Regex.Match(btc_buy_and_sell_Response, @"""price"":""+[0-9]+.[0-9]+""").Groups[0].Value;
                                    String btc_buy1 = System.Text.RegularExpressions.Regex.Match(btc_buy_and_sell_Response, @"""buy"",""state"":""wait"",""ord_type"":""limit"",""price"":""+[0-9]+.[0-9]+""").Groups[0].Value;
                                    String btc_sell = System.Text.RegularExpressions.Regex.Match(btc_sell1, @"[0-9]+\S+[0-9]").Groups[0].Value;
                                    String btc_buy = System.Text.RegularExpressions.Regex.Match(btc_buy1, @"[0-9]+\S+[0-9]").Groups[0].Value;

                                    String eth_buy_and_sell_Response = wc.DownloadString("https://api.abcc.com/api/v1/exchange/order_book?market_code=ethusdt");
                                    String eth_sell1 = System.Text.RegularExpressions.Regex.Match(eth_buy_and_sell_Response, @"""price"":""+[0-9]+.[0-9]+""").Groups[0].Value;
                                    String eth_buy1 = System.Text.RegularExpressions.Regex.Match(eth_buy_and_sell_Response, @"""buy"",""state"":""wait"",""ord_type"":""limit"",""price"":""+[0-9]+.[0-9]+""").Groups[0].Value;
                                    String eth_sell = System.Text.RegularExpressions.Regex.Match(eth_sell1, @"[0-9]+\S+[0-9]").Groups[0].Value;
                                    String eth_buy = System.Text.RegularExpressions.Regex.Match(eth_buy1, @"[0-9]+\S+[0-9]").Groups[0].Value;

                                    System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");

                                    Double Total = Convert.ToDouble(btc) * Convert.ToDouble(btc_buy) + Convert.ToDouble(eth) * Convert.ToDouble(eth_buy) + Convert.ToDouble(usdt);

                                    TimeSpan date = DateTime.Now.TimeOfDay;
                                    TimeSpan date1 = new TimeSpan(07, 00, 00);
                                    TimeSpan date2 = new TimeSpan(13, 00, 00);
                                    TimeSpan date3 = new TimeSpan(19, 00, 00);
                                    TimeSpan date4 = new TimeSpan(01, 00, 00);
                                    TimeSpan date5 = new TimeSpan(25, 00, 00);
                                    TimeSpan date6 = new TimeSpan(00, 00, 00);
                                    TimeSpan date7 = new TimeSpan(24, 00, 00);

                                    if (date1 >= date & date2 > date & date3 > date & date4 < date)
                                    {
                                        Data.LAST = date1.Subtract(date);
                                        Data.time = Data.LAST.ToString("hh':'mm':'ss");
                                    }

                                    if (date1 < date & date2 >= date & date3 > date & date4 < date)
                                    {
                                        Data.LAST = (date2.Subtract(date));
                                        Data.time = Data.LAST.ToString("hh':'mm':'ss");
                                    }
                                    if (date1 < date & date2 < date & date3 >= date & date4 < date)
                                    {
                                        Data.LAST = (date3.Subtract(date));
                                        Data.time = Data.LAST.ToString("hh':'mm':'ss");
                                    }

                                    if (date7 >= date & date >= date3)
                                    {
                                        Data.LAST = (date5.Subtract(date));
                                        Data.time = Data.LAST.ToString("hh':'mm':'ss");
                                    }
                                    if (date6 <= date & date4 >= date)
                                    {
                                        Data.LAST = (date4.Subtract(date));
                                        Data.time = Data.LAST.ToString("hh':'mm':'ss");
                                    }

                                    ///Знаки после запятой
                                    decimal Total11 = Convert.ToDecimal(Total.ToString("0.##"));
                                    double AT_ = Total / Data.VANGA;
                                    decimal AT_PRICE = Convert.ToDecimal(AT_.ToString("0.####"));

                                    double ETH_ = Convert.ToDouble(eth);
                                    decimal ETH_PRICE = Convert.ToDecimal(ETH_.ToString("0.####"));

                                    double BTC_ = Convert.ToDouble(btc);
                                    decimal BTC_PRICE = Convert.ToDecimal(BTC_.ToString("0.#######"));

                                    double USDT_ = Convert.ToDouble(usdt);
                                    decimal USDT_PRICE = Convert.ToDecimal(USDT_.ToString("0.##"));

                                    await Bot.DeleteMessageAsync(message.Chat.Id, message.MessageId);
                                    await Bot.SendTextMessageAsync(message.Chat.Id, "<code>TOTAL BLOCK:  " + Total11 + " USDT" + "\n" + "AT BLOCK PRICE: " + AT_PRICE + " USDT" + "\n" + "\n" + "BTC:  " + BTC_PRICE + "\n" + "ETH:  " + ETH_PRICE + "\n" + "USDT:  " + USDT_PRICE + "\n" + "\n" + "END BLOCK:  " + Data.time + "</code>", ParseMode.Html, false, false, 0, null);
                                }
                                catch
                                {

                                }
                            }
                            if (message.Text == "/at_usdt@ProAggressive_bot")
                            {
                                try
                                {
                                    System.Net.WebClient wc = new System.Net.WebClient();
                                    String at_buy_and_sell_Response = wc.DownloadString("https://api.abcc.com/api/v1/exchange/order_book?market_code=atusdt");
                                    String at_sell1 = System.Text.RegularExpressions.Regex.Match(at_buy_and_sell_Response, @"""price"":""+[0-9]+.[0-9]+""").Groups[0].Value;
                                    String at_sell = System.Text.RegularExpressions.Regex.Match(at_sell1, @"[0-9]+\S+[0-9]").Groups[0].Value;
                                    String at_buy1 = System.Text.RegularExpressions.Regex.Match(at_buy_and_sell_Response, @"""buy"",""state"":""wait"",""ord_type"":""limit"",""price"":""+[0-9]+.[0-9]+""").Groups[0].Value;
                                    String at_buy = System.Text.RegularExpressions.Regex.Match(at_buy1, @"[0-9]+\S+[0-9]").Groups[0].Value;

                                    var inlineKeyboardMarkup = new InlineKeyboardMarkup(new[]
                               {
                                        new [] { new InlineKeyboardButton { Text = "Перейти к паре AT/USDT", CallbackData = "demo", Url = "https://abcc.com/markets/atusdt?lang=en/" } }

                                });
                                    await Bot.DeleteMessageAsync(message.Chat.Id, message.MessageId);
                                    await Bot.SendTextMessageAsync(message.Chat.Id, "<b>AT/USDT</b>" + "\n" + "<code>Продажа: " + at_sell + "\n" + "Покупка: " + at_buy + "</code>" + "\n" + @"<a href=""https://abcc.com/markets/atusdt?lang=en/"">📈📈📈</a>", ParseMode.Html, false, false, 0, inlineKeyboardMarkup);

                                }
                                catch
                                {

                                }
                            }

                            if (message.Text == "/btc_usdt@ProAggressive_bot")
                            {
                                try
                                {
                                    System.Net.WebClient wc = new System.Net.WebClient();
                                    String btc_buy_and_sell_Response = wc.DownloadString("https://api.abcc.com/api/v1/exchange/order_book?market_code=btcusdt");
                                    String btc_sell1 = System.Text.RegularExpressions.Regex.Match(btc_buy_and_sell_Response, @"""price"":""+[0-9]+.[0-9]+""").Groups[0].Value;
                                    String btc_buy1 = System.Text.RegularExpressions.Regex.Match(btc_buy_and_sell_Response, @"""buy"",""state"":""wait"",""ord_type"":""limit"",""price"":""+[0-9]+.[0-9]+""").Groups[0].Value;
                                    String btc_sell = System.Text.RegularExpressions.Regex.Match(btc_sell1, @"[0-9]+\S+[0-9]").Groups[0].Value;
                                    String btc_buy = System.Text.RegularExpressions.Regex.Match(btc_buy1, @"[0-9]+\S+[0-9]").Groups[0].Value;
                                    var inlineKeyboardMarkup = new InlineKeyboardMarkup(new[]
                                    {
                                        new [] { new InlineKeyboardButton { Text = "Перейти к паре BTC/USDT", CallbackData = "demo", Url = "https://abcc.com/markets/btcusdt?lang=en/" } }
                                });
                                    await Bot.DeleteMessageAsync(message.Chat.Id, message.MessageId);
                                    await Bot.SendTextMessageAsync(message.Chat.Id, "<b>BTC/USDT</b>" + "\n" + "<code>Продажа: " + btc_sell + "\n" + "Покупка: " + btc_buy + "</code>" + "\n" + @"<a href=""https://abcc.com/markets/btcusdt?lang=en/"">📈📈📈</a>", ParseMode.Html, false, false, 0, inlineKeyboardMarkup);
                                }
                                catch
                                {

                                }
                            }
                            if (message.Text == "/eth_usdt@ProAggressive_bot")
                            {
                                try
                                {

                                    System.Net.WebClient wc = new System.Net.WebClient();
                                    String eth_buy_and_sell_Response = wc.DownloadString("https://api.abcc.com/api/v1/exchange/order_book?market_code=ethusdt");
                                    String eth_sell1 = System.Text.RegularExpressions.Regex.Match(eth_buy_and_sell_Response, @"""price"":""+[0-9]+.[0-9]+""").Groups[0].Value;
                                    String eth_buy1 = System.Text.RegularExpressions.Regex.Match(eth_buy_and_sell_Response, @"""buy"",""state"":""wait"",""ord_type"":""limit"",""price"":""+[0-9]+.[0-9]+""").Groups[0].Value;
                                    String eth_sell = System.Text.RegularExpressions.Regex.Match(eth_sell1, @"[0-9]+\S+[0-9]").Groups[0].Value;
                                    String eth_buy = System.Text.RegularExpressions.Regex.Match(eth_buy1, @"[0-9]+\S+[0-9]").Groups[0].Value;

                                    var inlineKeyboardMarkup = new InlineKeyboardMarkup(new[]
                                    {
                                         new [] { new InlineKeyboardButton { Text = "Перейти к паре ETH/USDT", CallbackData = "demo", Url = "https://abcc.com/markets/ethusdt?lang=en/" } }

                                });
                                    await Bot.DeleteMessageAsync(message.Chat.Id, message.MessageId);
                                    await Bot.SendTextMessageAsync(message.Chat.Id, "<b>ETH/USDT</b>" + "\n" + "<code>Продажа: " + eth_sell + "\n" + "Покупка: " + eth_buy + "</code>" + "\n" + @"<a href=""https://abcc.com/markets/ethusdt?lang=en/"">📈📈📈</a>", ParseMode.Html, false, false, 0, inlineKeyboardMarkup);

                                }
                                catch
                                {

                                }
                            }
                            if (message.Text == "/ltc_usdt@ProAggressive_bot")
                            {
                                try
                                {

                                    System.Net.WebClient wc = new System.Net.WebClient();
                                    String ltc_buy_and_sell_Response = wc.DownloadString("https://api.abcc.com/api/v1/exchange/order_book?market_code=ltcusdt");
                                    String ltc_sell1 = System.Text.RegularExpressions.Regex.Match(ltc_buy_and_sell_Response, @"""price"":""+[0-9]+.[0-9]+""").Groups[0].Value;
                                    String ltc_buy1 = System.Text.RegularExpressions.Regex.Match(ltc_buy_and_sell_Response, @"""buy"",""state"":""wait"",""ord_type"":""limit"",""price"":""+[0-9]+.[0-9]+""").Groups[0].Value;
                                    String ltc_sell = System.Text.RegularExpressions.Regex.Match(ltc_sell1, @"[0-9]+\S+[0-9]").Groups[0].Value;
                                    String ltc_buy = System.Text.RegularExpressions.Regex.Match(ltc_buy1, @"[0-9]+\S+[0-9]").Groups[0].Value;

                                    var inlineKeyboardMarkup = new InlineKeyboardMarkup(new[]
                                     {
                                         new [] { new InlineKeyboardButton { Text = "Перейти к паре LTC/USDT", CallbackData = "demo", Url = "https://abcc.com/markets/ltcusdt?lang=en/" } }
                                         }
                                    );
                                    await Bot.DeleteMessageAsync(message.Chat.Id, message.MessageId);
                                    await Bot.SendTextMessageAsync(message.Chat.Id, "<b>LTC/USDT</b>" + "\n" + "<code>Продажа: " + ltc_sell + "\n" + "Покупка: " + ltc_buy + "</code>" + "\n" + @"<a href=""https://abcc.com/markets/ltcusdt?lang=en/"">📈📈📈</a>", ParseMode.Html, false, false, 0, inlineKeyboardMarkup);

                                }
                                catch
                                {

                                }
                            }
                            if (message.Text == "/etc_usdt@ProAggressive_bot")
                            {
                                try
                                {

                                    System.Net.WebClient wc = new System.Net.WebClient();
                                    String etc_buy_and_sell_Response = wc.DownloadString("https://api.abcc.com/api/v1/exchange/order_book?market_code=etcusdt");
                                    String etc_sell1 = System.Text.RegularExpressions.Regex.Match(etc_buy_and_sell_Response, @"""price"":""+[0-9]+.[0-9]+""").Groups[0].Value;
                                    String etc_buy1 = System.Text.RegularExpressions.Regex.Match(etc_buy_and_sell_Response, @"""buy"",""state"":""wait"",""ord_type"":""limit"",""price"":""+[0-9]+.[0-9]+""").Groups[0].Value;
                                    String etc_sell = System.Text.RegularExpressions.Regex.Match(etc_sell1, @"[0-9]+\S+[0-9]").Groups[0].Value;
                                    String etc_buy = System.Text.RegularExpressions.Regex.Match(etc_buy1, @"[0-9]+\S+[0-9]").Groups[0].Value;

                                    var inlineKeyboardMarkup = new InlineKeyboardMarkup(new[]
                                     {
                                         new [] { new InlineKeyboardButton { Text = "Перейти к паре ETC/USDT", CallbackData = "demo", Url = "https://abcc.com/markets/etcusdt?lang=en/" } }
                                         }
                                    );
                                    await Bot.DeleteMessageAsync(message.Chat.Id, message.MessageId);
                                    await Bot.SendTextMessageAsync(message.Chat.Id, "<b>ETC/USDT</b>" + "\n" + "<code>Продажа: " + etc_sell + "\n" + "Покупка: " + etc_buy + "</code>" + "\n" + @"<a href=""https://abcc.com/markets/etcusdt?lang=en/"">📈📈📈</a>", ParseMode.Html, false, false, 0, inlineKeyboardMarkup);

                                }
                                catch
                                {

                                }
                            }
                            if (message.Text == "/ada_usdt@ProAggressive_bot")
                            {
                                try
                                {

                                    System.Net.WebClient wc = new System.Net.WebClient();
                                    String ada_buy_and_sell_Response = wc.DownloadString("https://api.abcc.com/api/v1/exchange/order_book?market_code=adausdt");
                                    String ada_sell1 = System.Text.RegularExpressions.Regex.Match(ada_buy_and_sell_Response, @"""price"":""+[0-9]+.[0-9]+""").Groups[0].Value;
                                    String ada_buy1 = System.Text.RegularExpressions.Regex.Match(ada_buy_and_sell_Response, @"""buy"",""state"":""wait"",""ord_type"":""limit"",""price"":""+[0-9]+.[0-9]+""").Groups[0].Value;
                                    String ada_sell = System.Text.RegularExpressions.Regex.Match(ada_sell1, @"[0-9]+\S+[0-9]").Groups[0].Value;
                                    String ada_buy = System.Text.RegularExpressions.Regex.Match(ada_buy1, @"[0-9]+\S+[0-9]").Groups[0].Value;

                                    var inlineKeyboardMarkup = new InlineKeyboardMarkup(new[]
                                     {
                                         new [] { new InlineKeyboardButton { Text = "Перейти к паре ADA/USDT", CallbackData = "demo", Url = "https://abcc.com/markets/adausdt?lang=en/" } }
                                         }
                                    );
                                    await Bot.DeleteMessageAsync(message.Chat.Id, message.MessageId);
                                    await Bot.SendTextMessageAsync(message.Chat.Id, "<b>ADA/USDT</b>" + "\n" + "<code>Продажа: " + ada_sell + "\n" + "Покупка: " + ada_buy + "</code>" + "\n" + @"<a href=""https://abcc.com/markets/adausdt?lang=en/"">📈📈📈</a>", ParseMode.Html, false, false, 0, inlineKeyboardMarkup);

                                }
                                catch
                                {

                                }
                            }
                            if (message.Text == "/eos_usdt@ProAggressive_bot")
                            {
                                try
                                {

                                    System.Net.WebClient wc = new System.Net.WebClient();
                                    String eos_buy_and_sell_Response = wc.DownloadString("https://api.abcc.com/api/v1/exchange/order_book?market_code=eosusdt");
                                    String eos_sell1 = System.Text.RegularExpressions.Regex.Match(eos_buy_and_sell_Response, @"""price"":""+[0-9]+.[0-9]+""").Groups[0].Value;
                                    String eos_buy1 = System.Text.RegularExpressions.Regex.Match(eos_buy_and_sell_Response, @"""buy"",""state"":""wait"",""ord_type"":""limit"",""price"":""+[0-9]+.[0-9]+""").Groups[0].Value;
                                    String eos_sell = System.Text.RegularExpressions.Regex.Match(eos_sell1, @"[0-9]+\S+[0-9]").Groups[0].Value;
                                    String eos_buy = System.Text.RegularExpressions.Regex.Match(eos_buy1, @"[0-9]+\S+[0-9]").Groups[0].Value;

                                    var inlineKeyboardMarkup = new InlineKeyboardMarkup(new[]
                                     {
                                         new [] { new InlineKeyboardButton { Text = "Перейти к паре EOS/USDT", CallbackData = "demo", Url = "https://abcc.com/markets/eosusdt?lang=en/" } }
                                         }
                                    );
                                    await Bot.DeleteMessageAsync(message.Chat.Id, message.MessageId);
                                    await Bot.SendTextMessageAsync(message.Chat.Id, "<b>EOS/USDT</b>" + "\n" + "<code>Продажа: " + eos_sell + "\n" + "Покупка: " + eos_buy + "</code>" + "\n" + @"<a href=""https://abcc.com/markets/eosusdt?lang=en/"">📈📈📈</a>", ParseMode.Html, false, false, 0, inlineKeyboardMarkup);

                                }
                                catch
                                {

                                }
                            }
                            if (message.Text == "/xrp_usdt@ProAggressive_bot")
                            {
                                try
                                {

                                    System.Net.WebClient wc = new System.Net.WebClient();
                                    String xrp_buy_and_sell_Response = wc.DownloadString("https://api.abcc.com/api/v1/exchange/order_book?market_code=xrpusdt");
                                    String xrp_sell1 = System.Text.RegularExpressions.Regex.Match(xrp_buy_and_sell_Response, @"""price"":""+[0-9]+.[0-9]+""").Groups[0].Value;
                                    String xrp_buy1 = System.Text.RegularExpressions.Regex.Match(xrp_buy_and_sell_Response, @"""buy"",""state"":""wait"",""ord_type"":""limit"",""price"":""+[0-9]+.[0-9]+""").Groups[0].Value;
                                    String xrp_sell = System.Text.RegularExpressions.Regex.Match(xrp_sell1, @"[0-9]+\S+[0-9]").Groups[0].Value;
                                    String xrp_buy = System.Text.RegularExpressions.Regex.Match(xrp_buy1, @"[0-9]+\S+[0-9]").Groups[0].Value;

                                    var inlineKeyboardMarkup = new InlineKeyboardMarkup(new[]
                                    {
                                         new [] { new InlineKeyboardButton { Text = "Перейти к паре XRP/USDT", CallbackData = "demo", Url = "https://abcc.com/markets/xrpusdt?lang=en/" } }
                                         }
                                    );
                                    await Bot.DeleteMessageAsync(message.Chat.Id, message.MessageId);
                                    await Bot.SendTextMessageAsync(message.Chat.Id, "<b>XRP/USDT</b>" + "\n" + "<code>Продажа: " + xrp_sell + "\n" + "Покупка: " + xrp_buy + "</code>" + "\n" + @"<a href=""https://abcc.com/markets/xrpusdt?lang=en/"">📈📈📈</a>", ParseMode.Html, false, false, 0, inlineKeyboardMarkup);

                                }
                                catch
                                {

                                }
                            }
                            if (message.Text == "/qtum_usdt@ProAggressive_bot")
                            {
                                try
                                {

                                    System.Net.WebClient wc = new System.Net.WebClient();
                                    String qtum_buy_and_sell_Response = wc.DownloadString("https://api.abcc.com/api/v1/exchange/order_book?market_code=qtumusdt");
                                    String qtum_sell1 = System.Text.RegularExpressions.Regex.Match(qtum_buy_and_sell_Response, @"""price"":""+[0-9]+.[0-9]+""").Groups[0].Value;
                                    String qtum_buy1 = System.Text.RegularExpressions.Regex.Match(qtum_buy_and_sell_Response, @"""buy"",""state"":""wait"",""ord_type"":""limit"",""price"":""+[0-9]+.[0-9]+""").Groups[0].Value;
                                    String qtum_sell = System.Text.RegularExpressions.Regex.Match(qtum_sell1, @"[0-9]+\S+[0-9]").Groups[0].Value;
                                    String qtum_buy = System.Text.RegularExpressions.Regex.Match(qtum_buy1, @"[0-9]+\S+[0-9]").Groups[0].Value;

                                    var inlineKeyboardMarkup = new InlineKeyboardMarkup(new[]
                                     {
                                         new [] { new InlineKeyboardButton { Text = "Перейти к паре QTUM/USDT", CallbackData = "demo", Url = "https://abcc.com/markets/qtumusdt?lang=en/" } }
                                         }
                                    );
                                    await Bot.DeleteMessageAsync(message.Chat.Id, message.MessageId);
                                    await Bot.SendTextMessageAsync(message.Chat.Id, "<b>QTUM/USDT</b>" + "\n" + "<code>Продажа: " + qtum_sell + "\n" + "Покупка: " + qtum_buy + "</code>" + "\n" + @"<a href=""https://abcc.com/markets/qtumusdt?lang=en/"">📈📈📈</a>", ParseMode.Html, false, false, 0, inlineKeyboardMarkup);

                                }
                                catch
                                {

                                }
                            }
                            if (message.Text == "/zrx_usdt@ProAggressive_bot")
                            {
                                try
                                {

                                    System.Net.WebClient wc = new System.Net.WebClient();
                                    String zrx_buy_and_sell_Response = wc.DownloadString("https://api.abcc.com/api/v1/exchange/order_book?market_code=zrxusdt");
                                    String zrx_sell1 = System.Text.RegularExpressions.Regex.Match(zrx_buy_and_sell_Response, @"""price"":""+[0-9]+.[0-9]+""").Groups[0].Value;
                                    String zrx_buy1 = System.Text.RegularExpressions.Regex.Match(zrx_buy_and_sell_Response, @"""buy"",""state"":""wait"",""ord_type"":""limit"",""price"":""+[0-9]+.[0-9]+""").Groups[0].Value;
                                    String zrx_sell = System.Text.RegularExpressions.Regex.Match(zrx_sell1, @"[0-9]+\S+[0-9]").Groups[0].Value;
                                    String zrx_buy = System.Text.RegularExpressions.Regex.Match(zrx_buy1, @"[0-9]+\S+[0-9]").Groups[0].Value;

                                    var inlineKeyboardMarkup = new InlineKeyboardMarkup(new[]
                                      {
                                         new [] { new InlineKeyboardButton { Text = "Перейти к паре ZRX/USDT", CallbackData = "demo", Url = "https://abcc.com/markets/zrxusdt?lang=en/" } }
                                         }
                                    );
                                    await Bot.DeleteMessageAsync(message.Chat.Id, message.MessageId);
                                    await Bot.SendTextMessageAsync(message.Chat.Id, "<b>ZRX/USDT</b>" + "\n" + "<code>Продажа: " + zrx_sell + "\n" + "Покупка: " + zrx_buy + "</code>" + "\n" + @"<a href=""https://abcc.com/markets/zrxusdt?lang=en/"">📈📈📈</a>", ParseMode.Html, false, false, 0, inlineKeyboardMarkup);

                                }
                                catch
                                {

                                }
                            }
                           
                            if (message.Text == "/dash_usdt@ProAggressive_bot")
                            {
                                try
                                {

                                    System.Net.WebClient wc = new System.Net.WebClient();
                                    String dash_buy_and_sell_Response = wc.DownloadString("https://api.abcc.com/api/v1/exchange/order_book?market_code=dashusdt");
                                    String dash_sell1 = System.Text.RegularExpressions.Regex.Match(dash_buy_and_sell_Response, @"""price"":""+[0-9]+.[0-9]+""").Groups[0].Value;
                                    String dash_buy1 = System.Text.RegularExpressions.Regex.Match(dash_buy_and_sell_Response, @"""buy"",""state"":""wait"",""ord_type"":""limit"",""price"":""+[0-9]+.[0-9]+""").Groups[0].Value;
                                    String dash_sell = System.Text.RegularExpressions.Regex.Match(dash_sell1, @"[0-9]+\S+[0-9]").Groups[0].Value;
                                    String dash_buy = System.Text.RegularExpressions.Regex.Match(dash_buy1, @"[0-9]+\S+[0-9]").Groups[0].Value;

                                    var inlineKeyboardMarkup = new InlineKeyboardMarkup(new[]
                                     {
                                         new [] { new InlineKeyboardButton { Text = "Перейти к паре DASH/USDT", CallbackData = "demo", Url = "https://abcc.com/markets/dashusdt?lang=en/" } }
                                         }
                                    );
                                    await Bot.DeleteMessageAsync(message.Chat.Id, message.MessageId);
                                    await Bot.SendTextMessageAsync(message.Chat.Id, "<b>DASH/USDT</b>" + "\n" + "<code>Продажа: " + dash_sell + "\n" + "Покупка: " + dash_buy + "</code>" + "\n" + @"<a href=""https://abcc.com/markets/dashusdt?lang=en/"">📈📈📈</a>", ParseMode.Html, false, false, 0, inlineKeyboardMarkup);

                                }
                                catch
                                {

                                }
                            }
                            if (message.Text == "/omg_usdt@ProAggressive_bot")
                            {
                                try
                                {

                                    System.Net.WebClient wc = new System.Net.WebClient();
                                    String omg_buy_and_sell_Response = wc.DownloadString("https://api.abcc.com/api/v1/exchange/order_book?market_code=omgusdt");
                                    String omg_sell1 = System.Text.RegularExpressions.Regex.Match(omg_buy_and_sell_Response, @"""price"":""+[0-9]+.[0-9]+""").Groups[0].Value;
                                    String omg_buy1 = System.Text.RegularExpressions.Regex.Match(omg_buy_and_sell_Response, @"""buy"",""state"":""wait"",""ord_type"":""limit"",""price"":""+[0-9]+.[0-9]+""").Groups[0].Value;
                                    String omg_sell = System.Text.RegularExpressions.Regex.Match(omg_sell1, @"[0-9]+\S+[0-9]").Groups[0].Value;
                                    String omg_buy = System.Text.RegularExpressions.Regex.Match(omg_buy1, @"[0-9]+\S+[0-9]").Groups[0].Value;


                                    var inlineKeyboardMarkup = new InlineKeyboardMarkup(new[]
                                    {
                                         new [] { new InlineKeyboardButton { Text = "Перейти к паре OMG/USDT", CallbackData = "demo", Url = "https://abcc.com/markets/omgusdt?lang=en/" } }
                                         }
                                    );
                                    await Bot.DeleteMessageAsync(message.Chat.Id, message.MessageId);
                                    await Bot.SendTextMessageAsync(message.Chat.Id, "<b>OMG/USDT</b>" + "\n" + "<code>Продажа: " + omg_sell + "\n" + "Покупка: " + omg_buy + "</code>" + "\n" + @"<a href=""https://abcc.com/markets/omgusdt?lang=en/"">📈📈📈</a>", ParseMode.Html, false, false, 0, inlineKeyboardMarkup);

                                }
                                catch
                                {

                                }
                            }
                            if (message.Text == "Linda" || message.Text == "linda" || message.Text == "LindaMao")
                            {
                                try
                                {
                                    //@LindaMao

                                    await Bot.SendTextMessageAsync(message.Chat.Id, "♥♥♥ I love @LindaMao ♥♥♥");
                                }
                                catch
                                {
                                }
                            }

                            if (message.Text == "Xini" || message.Text == "xini" || message.Text == "xw" || message.Text == "XW" || message.Text == "XiniW")
                            {
                                try
                                {
                                    //@XiniW
                                    await Bot.SendTextMessageAsync(message.Chat.Id, "♥♥♥ I love @XiniW ♥♥♥");
                                }
                                catch
                                {

                                }

                            }
                            if (message.Text == "/at_token@ProAggressive_bot")
                            {
                                try
                                {// в ответ на команду /saysomething выводим сообщение
                                    await Bot.DeleteMessageAsync(message.Chat.Id, message.MessageId);
                                    await Bot.SendTextMessageAsync(message.Chat.Id, "ABCC Token (AT) — это токены стандарта ERC20. Всего будет выпущено 210 миллионов AT, это фиксированное количество, дальнейший выпуск монет производиться не будет. То есть, AT дефляционный токен, что в будущем положительно скажется на его стоимости.", ParseMode.Html, false, false, 0, null);
                                }
                                catch
                                {

                                }
                            }
                            if (message.Text == "/chat_info@ProAggressive_bot")
                            {
                                try
                                {
                                    var inlineKeyboardMarkup = new InlineKeyboardMarkup(new[]
                                   {
                                         new [] { new InlineKeyboardButton { Text = "Перейти на биржу!", CallbackData = "demo", Url = "https://abcc.com/" } }
                                         }
                                   );
                                    await Bot.DeleteMessageAsync(message.Chat.Id, message.MessageId);
                                    await Bot.SendTextMessageAsync(message.Chat.Id, "ABCC СНГ (@abcc_rus) - лучший чат для руcскоговорящего сегмента биржи.Наша цель - создать мощное комьюнити, которое будет помогать бирже развиваться.Здесь вы можете узнать абсолютно всё касаемо ABCC, от последних новостей до инсайдов о грядущих изменениях, от первых шагов на бирже до вывода средств!В режиме реального времени вы можете задать любой вопрос и мы с радостью поможем и дадим рекомендацию.Присоединяйтесь к нашему чату!Мы ценим каждого пользователя и будем рады всем: трейдерам, инвесторам или холдерам из СНГ!", ParseMode.Html, false, false, 0, inlineKeyboardMarkup);
                                }
                                catch
                                {

                                }
                            }

                            if (message.Text == "/deposit@ProAggressive_bot")
                            {
                                try
                                {
                                    await Bot.DeleteMessageAsync(message.Chat.Id, message.MessageId);
                                    await Bot.SendTextMessageAsync(message.Chat.Id, "Чтобы пополнить счет, перейди в «Мой капитал» ⇒ «Депозиты» (Deposits). Затем выбери криптовалюту, счет которой хочешь пополнить. Нажав на кнопку «Депозит», ты увидишь адрес кошелька. Переведи монеты на этот адрес. Платеж зачислится на ваш счет после двух подтверждений сети. Вы можете проверить транзакцию, перейдя на вкладку «История» на странице «Мой капитал».", ParseMode.Html, false, false, 0, null);
                                }
                                catch
                                {

                                }
                            }

                            if (message.Text == "/mining@ProAggressive_bot")
                            {
                                try
                                {
                                    await Bot.DeleteMessageAsync(message.Chat.Id, message.MessageId);
                                    await Bot.SendTextMessageAsync(message.Chat.Id, "Токен использует  новую стратегию выпуска Trade-to-Mine (ToM). Изначально не было никакого ICO. Каждый 6-часовой блок генерируется определенное количеcтво AT токенов, на следующие сутки сгенерированные токены распределяются прямо пропорционально сумме, потраченной пользователями на комиссию.Например, если за блок дают 87500 AT и всего во время блока было собрано 25000$ комиссий, тогда за каждые 25000 / 87500 ≈ 0, 2857$ комиссии ≈ 285, 7$ оброта будет выдан 1 АТ токен.Примечание: в примере расчета не учтены ваучеры на увеличение мощности майнинга и реферальные отчисления.", ParseMode.Html, false, false, 0, null);
                                }
                                catch
                                {

                                }
                            }

                            if (message.Text == "/withdrawal@ProAggressive_bot")
                            {
                                try
                                {
                                    await Bot.DeleteMessageAsync(message.Chat.Id, message.MessageId);
                                    await Bot.SendTextMessageAsync(message.Chat.Id, "Вывод денег осуществляется из меню «Мой капитал» ⇒ «Снятие». Нажми «Снятие» (Withdraw) и ты попадешь на страницу, где сможешь выбрать монеты для совершения вывода средств. Введи адрес, на который хочешь вывести средства, и сумму, которую нужно снять. Нажми «Подтвердить заказ», а затем проверь свою электронную почту для подтверждения вывода. Нажми «Подтвердить снятие» в письме. Как и в случае с депозитами, состояние транзакции можно просмотреть во вкладке «История». Лимиты снятия средств на ABCC При использовании ABCC существует лимит на вывод средств.Те, кто не прошел верификацию, могут снять только до 2 BTC в течение суток.Те, кто верифицировал аккаунт, могут снять до 200 BTC в течение 24 часов.Если вам нужен более высокий лимит снятия средств, вы можете связаться с поддержкой, и они рассмотрят вашу ситуацию в индивидуальном порядке.", ParseMode.Html, false, false, 0, null);
                                }
                                catch
                                {

                                }
                            }

                            if (message.Text == "/commission@ProAggressive_bot")
                            {
                                try
                                {
                                    await Bot.DeleteMessageAsync(message.Chat.Id, message.MessageId);
                                    await Bot.SendTextMessageAsync(message.Chat.Id, "Торговая платформа ABCC действительно удобна для пользователей и предлагает хороший выбор торговых пар, а также подробные диаграммы для руководства решениями о покупке и продаже. Система поощрения хранения токенов AT дает хороший стимул для людей, чтобы они держали токен и получили за это часть прибыли платформы. ABCC имеет все шансы стать лидером в среде крипто бирж.", ParseMode.Html, false, false, 0, null);
                                }
                                catch
                                {

                                }
                            }

                            if (message.Text == "/verification@ProAggressive_bot")
                            {
                                try
                                {
                                    await Bot.DeleteMessageAsync(message.Chat.Id, message.MessageId);
                                    await Bot.SendTextMessageAsync(message.Chat.Id, "Перейди в верхний правый угол экрана, щелкни на иконку человека и нажми «Проверка» (перед этим выставьте русский язык). Это приведет вас к странице, на которой вы можете подтвердить свою учетную запись. В первую очередь укажите страну проживания. Затем вам нужно будет ввести свое имя, номер паспорта, а также загрузить серию фотографий с информацией о вашем паспорте. Обычно заявки на верификацию обрабатывается за один рабочий день. Однако может потребоваться больше времени, если есть большой объем пользователей, запросивших верификацию.", ParseMode.Html, false, false, 0, null);

                                }
                                catch
                                {

                                }
                            }

                            if (message.Text == "/voucher@ProAggressive_bot")
                            {
                                try
                                {
                                    await Bot.DeleteMessageAsync(message.Chat.Id, message.MessageId);
                                    await Bot.SendTextMessageAsync(message.Chat.Id, "Ваучер - это уникальное преимущество для пользователей ABCC. Те, у кого есть ваучеры, могут пользоваться более высокой мощностью добычи АТ по той же цене.Пользователи получают ваучеры в разных значениях за хранение AT за последние 7 дней.Ваучер, который вы получите = Самые низкие запасы АТ за 7 дней / Самые низкие запасы АТ всех пользователей за 7 дней * Итоговая выписка дневного купона на повышение стоимости.Ваучер дает увеличение добычи на опредленный обьем торгов, который не обязательно выполнять до конца.", ParseMode.Html, false, false, 0, null);

                                }
                                catch
                                {

                                }
                            }

                            if (message.Text == "/minimal_trade@ProAggressive_bot")
                            {
                                try
                                {
                                    await Bot.DeleteMessageAsync(message.Chat.Id, message.MessageId);
                                    await Bot.SendTextMessageAsync(message.Chat.Id, "Минимальные суммы для совершения сделки" + "\n" + "10 USDT" + "\n" + "0.01 ETH" + "\n" + "0.001 BTC", ParseMode.Html, false, false, 0, null);

                                }
                                catch
                                {

                                }

                            }
                            if (message.Text == "/option@ProAggressive_bot")
                            {
                                try
                                {
                                    var inlineKeyboardMarkup = new InlineKeyboardMarkup(new[]
                                 {

                                      new  [] { new InlineKeyboardButton { Text = "Больше о опционах!", CallbackData = "demo", Url = "https://abcc.com/option/about" } }

                                });


                                    await Bot.DeleteMessageAsync(message.Chat.Id, message.MessageId);
                                    await Bot.SendTextMessageAsync(message.Chat.Id, "👉Пример работы опционов, самый простой:" + "\n"   + "\n" + "🍒Всё это просто объяснить, чтобы всем понятно было!Между двумя людьми заключается сделка - один ставит на то, что цена биткоина будет ниже, другой на то что выше, кто угадал тому профит. " + "\n" + "\n"+ "🍓Берём двух людей у каждого по 100 АТ, один даёт 100АТ на заморозку ставя,что цена будет ниже, другой покупает по цене которую они определили, например 0.5 АТ за контракт, таким образом он покупает 100 контрактов у первого чела за 50АТ, эти 50АТ идут первому челу, и там уже кто выиграл тому и достаются 100 замороженных АТ! В блок со всего этого дела идёт 0.5 % за каждое движение, а победа зависит от цены биткоина. 📊📈🔥🚀" +"\n"+"\n" + "Также есть более подробная инфа на английском:", ParseMode.Html, false, false, 0, inlineKeyboardMarkup);

                                }
                                catch
                                {

                                }

                            }
                            if (message.Text == "/price_option@ProAggressive_bot")
                            {
                                try
                                {

                                    var inlineMainKeyboard = new InlineKeyboardMarkup(new[]
                    {
                        new []
                        {
                           new InlineKeyboardButton  { Text = "Call Daily Options ", CallbackData = "CallDaily"  },

                          new   InlineKeyboardButton { Text = "Put Daily Options ", CallbackData = "PutDaily" } },
                         new []
                        {
                           new InlineKeyboardButton  { Text = "Call 6Hr Daily Options ", CallbackData = "Call6Hr"  },

                          new   InlineKeyboardButton { Text = "Put 6Hr Daily Options ", CallbackData = "Put6Hr" } }
                                    });


                                    string Current_price, Exercise_Price, Change, Potential, Contracts;
                                    await Bot.DeleteMessageAsync(message.Chat.Id, message.MessageId);

                                    await Bot.SendTextMessageAsync(message.Chat.Id,"<b>Цена на опционы!</b>", ParseMode.Html, false, false, 0, inlineMainKeyboard);



                                }
                                catch
                                {

                                }

                            }

                        }



                        if (message.Type == MessageType.ChatMemberLeft)
                        {
                            try
                            {
                                await Bot.DeleteMessageAsync(message.Chat.Id, message.MessageId);
                            }
                            catch
                            {

                            }
                            return;

                        }
                        if (Data.HI==1 & message.Type == MessageType.ChatMembersAdded)
                        {
                            try
                            {
                                await Bot.DeleteMessageAsync(message.Chat.Id, message.MessageId);
                            }
                            catch
                            {

                            }
                            return;

                        }
                        if (message.Text == "/stop_hello_func@ProAggressive_bot")
                        {
                            try
                            {
                                Data.HI = 1;
                            }


                            catch
                            {

                            }
                        }
                        if (message.Text == "/start_hello_func@ProAggressive_bot")
                        {
                            try
                            {
                                Data.HI = 0;
                            }


                            catch
                            {

                            }
                        }

                        if (Data.HI == 0 & message.Type == MessageType.ChatMembersAdded)
                        {
                            try
                            {
                                var inlineKeyboardMarkup = new InlineKeyboardMarkup(new[]
                                {

                                      new  [] { new InlineKeyboardButton { Text = "Скачать мобильное приложение!", CallbackData = "demo", Url = "https://medium.com/@abcc.com/our-mobile-app-is-ready-41fdd6663869 " } }

                                });
                                if (Hi.one == null)
                                {
                                    if (update.Message.From.Username != null)
                                    {
                                        string one = update.Message.From.Username;
                                        Hi.one = "@" + one;
                                        await Bot.DeleteMessageAsync(message.Chat.Id, message.MessageId);
                                        return;
                                    }
                                    else
                                    {
                                        string one = update.Message.From.FirstName + " " + update.Message.From.LastName;
                                        Hi.one = one;
                                        await Bot.DeleteMessageAsync(message.Chat.Id, message.MessageId);
                                        return;
                                    }
                                }

                                if (Hi.two == null)
                                {
                                    if (update.Message.From.Username != null)
                                    {
                                        string two = update.Message.From.Username;
                                        Hi.two = "@" + two;
                                        await Bot.DeleteMessageAsync(message.Chat.Id, message.MessageId);
                                        return;
                                    }
                                    else
                                    {
                                        string two = update.Message.From.FirstName + " " + update.Message.From.LastName;
                                        Hi.two = two;
                                        await Bot.DeleteMessageAsync(message.Chat.Id, message.MessageId);
                                        return;
                                    }
                                }
                                if (Hi.three == null)
                                {
                                    if (update.Message.From.Username != null)
                                    {
                                        string three = update.Message.From.Username;
                                        Hi.three = "@" + three;
                                        await Bot.DeleteMessageAsync(message.Chat.Id, message.MessageId);
                                        return;
                                    }
                                    else
                                    {
                                        string three = update.Message.From.FirstName + " " + update.Message.From.LastName;
                                        Hi.three = three;
                                        await Bot.DeleteMessageAsync(message.Chat.Id, message.MessageId);
                                        return;
                                    }
                                }
                                if (Hi.four == null)
                                {
                                    if (update.Message.From.Username != null)
                                    {
                                        string four = update.Message.From.Username;
                                        Hi.four = "@" + four;
                                        await Bot.DeleteMessageAsync(message.Chat.Id, message.MessageId);
                                        return;
                                    }
                                    else
                                    {
                                        string four = update.Message.From.FirstName + " " + update.Message.From.LastName;
                                        Hi.four = four;
                                        await Bot.DeleteMessageAsync(message.Chat.Id, message.MessageId);
                                        return;
                                    }
                                }
                                if (Hi.five == null)
                                {
                                    if (update.Message.From.Username != null)
                                    {
                                        string five = update.Message.From.Username;
                                        Hi.five = "@" + five;
                                        await Bot.DeleteMessageAsync(message.Chat.Id, message.MessageId);
                                        return;
                                    }
                                    else
                                    {
                                        string five = update.Message.From.FirstName + " " + update.Message.From.LastName;
                                        Hi.five = five;
                                        await Bot.DeleteMessageAsync(message.Chat.Id, message.MessageId);
                                        return;
                                    }
                                }
                                if (Hi.six == null)
                                {
                                    if (update.Message.From.Username != null)
                                    {
                                        string six = update.Message.From.Username;
                                        Hi.six = "@" + six;
                                        await Bot.DeleteMessageAsync(message.Chat.Id, message.MessageId);
                                        return;
                                    }
                                    else
                                    {
                                        string six = update.Message.From.FirstName + " " + update.Message.From.LastName;
                                        Hi.six = six;
                                        await Bot.DeleteMessageAsync(message.Chat.Id, message.MessageId);
                                        return;
                                    }
                                }
                                if (Hi.seven == null)
                                {
                                    if (update.Message.From.Username != null)
                                    {
                                        string seven = update.Message.From.Username;
                                        Hi.seven = "@" + seven;
                                        await Bot.DeleteMessageAsync(message.Chat.Id, message.MessageId);
                                        return;
                                    }
                                    else
                                    {
                                        string seven = update.Message.From.FirstName + " " + update.Message.From.LastName;
                                        Hi.seven = seven;
                                        await Bot.DeleteMessageAsync(message.Chat.Id, message.MessageId);
                                        return;
                                    }
                                }
                                if (Hi.eight == null)
                                {
                                    if (update.Message.From.Username != null)
                                    {
                                        string eight = update.Message.From.Username;
                                        Hi.eight = "@" + eight;
                                        await Bot.DeleteMessageAsync(message.Chat.Id, message.MessageId);
                                        return;
                                    }
                                    else
                                    {
                                        string eight = update.Message.From.FirstName + " " + update.Message.From.LastName;
                                        Hi.eight = eight;
                                        await Bot.DeleteMessageAsync(message.Chat.Id, message.MessageId);
                                        return;
                                    }
                                }
                                if (Hi.nine == null)
                                {
                                    if (update.Message.From.Username != null)
                                    {
                                        string nine = update.Message.From.Username;
                                        Hi.nine = "@" + nine;
                                        await Bot.DeleteMessageAsync(message.Chat.Id, message.MessageId);
                                        return;
                                    }
                                    else
                                    {
                                        string nine = update.Message.From.FirstName + " " + update.Message.From.LastName;
                                        Hi.nine = nine;
                                        await Bot.DeleteMessageAsync(message.Chat.Id, message.MessageId);
                                        return;
                                    }
                                }
                                if (Hi.ten == null)
                                {
                                    if (update.Message.From.Username != null)
                                    {
                                        string ten = update.Message.From.Username;
                                        Hi.ten = "@" + ten;
                                        await Bot.DeleteMessageAsync(message.Chat.Id, message.MessageId);
                                        return;
                                    }
                                    else
                                    {
                                        string ten = update.Message.From.FirstName + " " + update.Message.From.LastName;
                                        Hi.ten = ten;
                                        await Bot.DeleteMessageAsync(message.Chat.Id, message.MessageId);
                                        return;
                                    }
                                }
                                if (Hi.H11 == null)
                                {
                                    if (update.Message.From.Username != null)
                                    {
                                        string H11 = update.Message.From.Username;
                                        Hi.H11 = "@" + H11;
                                        await Bot.DeleteMessageAsync(message.Chat.Id, message.MessageId);
                                        return;
                                    }
                                    else
                                    {
                                        string H11 = update.Message.From.FirstName + " " + update.Message.From.LastName;
                                        Hi.H11 = H11;
                                        await Bot.DeleteMessageAsync(message.Chat.Id, message.MessageId);
                                        return;
                                    }
                                }
                                if (Hi.H12 == null)
                                {
                                    if (update.Message.From.Username != null)
                                    {
                                        string H12 = update.Message.From.Username;
                                        Hi.H12 = "@" + H12;
                                        await Bot.DeleteMessageAsync(message.Chat.Id, message.MessageId);
                                        return;
                                    }
                                    else
                                    {
                                        string H12 = update.Message.From.FirstName + " " + update.Message.From.LastName;
                                        Hi.H12 = H12;
                                        await Bot.DeleteMessageAsync(message.Chat.Id, message.MessageId);
                                        return;
                                    }
                                }
                                if (Hi.H13 == null)
                                {
                                    if (update.Message.From.Username != null)
                                    {
                                        string H13 = update.Message.From.Username;
                                        Hi.H13 = "@" + H13;
                                        await Bot.DeleteMessageAsync(message.Chat.Id, message.MessageId);
                                        return;
                                    }
                                    else
                                    {
                                        string H13 = update.Message.From.FirstName + " " + update.Message.From.LastName;
                                        Hi.H13 = H13;
                                        await Bot.DeleteMessageAsync(message.Chat.Id, message.MessageId);
                                        return;
                                    }
                                }
                                if (Hi.H14 == null)
                                {
                                    if (update.Message.From.Username != null)
                                    {
                                        string H14 = update.Message.From.Username;
                                        Hi.H14 = "@" + H14;
                                        await Bot.DeleteMessageAsync(message.Chat.Id, message.MessageId);
                                        return;
                                    }
                                    else
                                    {
                                        string H14 = update.Message.From.FirstName + " " + update.Message.From.LastName;
                                        Hi.H14 = H14;
                                        await Bot.DeleteMessageAsync(message.Chat.Id, message.MessageId);
                                        return;
                                    }
                                }
                                if (Hi.H15 == null)
                                {
                                    if (update.Message.From.Username != null)
                                    {
                                        string H15 = update.Message.From.Username;
                                        Hi.H15 = "@" + H15;
                                        await Bot.DeleteMessageAsync(message.Chat.Id, message.MessageId);
                                        return;
                                    }
                                    else
                                    {
                                        string H15 = update.Message.From.FirstName + " " + update.Message.From.LastName;
                                        Hi.H15 = H15;
                                        await Bot.DeleteMessageAsync(message.Chat.Id, message.MessageId);
                                        return;
                                    }
                                }
                                if (Hi.H16 == null)
                                {
                                    if (update.Message.From.Username != null)
                                    {
                                        string H16 = update.Message.From.Username;
                                        Hi.H16 = "@" + H16;
                                        await Bot.DeleteMessageAsync(message.Chat.Id, message.MessageId);
                                        return;
                                    }
                                    else
                                    {
                                        string H16 = update.Message.From.FirstName + " " + update.Message.From.LastName;
                                        Hi.H16 = H16;
                                        await Bot.DeleteMessageAsync(message.Chat.Id, message.MessageId);
                                        return;
                                    }
                                }
                                if (Hi.H17 == null)
                                {
                                    if (update.Message.From.Username != null)
                                    {
                                        string H17 = update.Message.From.Username;
                                        Hi.H17 = "@" + H17;
                                        await Bot.DeleteMessageAsync(message.Chat.Id, message.MessageId);
                                        return;
                                    }
                                    else
                                    {
                                        string H17 = update.Message.From.FirstName + " " + update.Message.From.LastName;
                                        Hi.H17 = H17;
                                        await Bot.DeleteMessageAsync(message.Chat.Id, message.MessageId);
                                        return;
                                    }
                                }
                                if (Hi.H17 == null)
                                {
                                    if (update.Message.From.Username != null)
                                    {
                                        string H17 = update.Message.From.Username;
                                        Hi.H17 = "@" + H17;
                                        await Bot.DeleteMessageAsync(message.Chat.Id, message.MessageId);
                                        return;
                                    }
                                    else
                                    {
                                        string H17 = update.Message.From.FirstName + " " + update.Message.From.LastName;
                                        Hi.H17 = H17;
                                        await Bot.DeleteMessageAsync(message.Chat.Id, message.MessageId);
                                        return;
                                    }
                                }
                                if (Hi.H18 == null)
                                {
                                    if (update.Message.From.Username != null)
                                    {
                                        string H18 = update.Message.From.Username;
                                        Hi.H18 = "@" + H18;
                                        await Bot.DeleteMessageAsync(message.Chat.Id, message.MessageId);
                                        return;
                                    }
                                    else
                                    {
                                        string H18 = update.Message.From.FirstName + " " + update.Message.From.LastName;
                                        Hi.H18 = H18;
                                        await Bot.DeleteMessageAsync(message.Chat.Id, message.MessageId);
                                        return;
                                    }
                                }
                                if (Hi.H19 == null)
                                {
                                    if (update.Message.From.Username != null)
                                    {
                                        string H19 = update.Message.From.Username;
                                        Hi.H19 = "@" + H19;

                                        await Bot.DeleteMessageAsync(message.Chat.Id, message.MessageId);
                                        return;
                                    }
                                    else
                                    {
                                        string H19 = update.Message.From.FirstName + " " + update.Message.From.LastName;
                                        Hi.H19 = H19;

                                        await Bot.DeleteMessageAsync(message.Chat.Id, message.MessageId);
                                        return;
                                    }
                                }
                                if (Hi.H20 == null)
                                {
                                    if (update.Message.From.Username != null)
                                    {
                                        string H20 = update.Message.From.Username;
                                        Hi.H20 = "@" + H20;
                                        await Bot.DeleteMessageAsync(message.Chat.Id, message.MessageId);
                                        await Bot.SendTextMessageAsync(message.Chat.Id, $"{Hi.one + ", " + Hi.two + ", " + Hi.three + ", " + Hi.four + ", " + Hi.five + ", " + Hi.six + ", " + Hi.seven + ", " + Hi.eight + ", " + Hi.nine + ", " + Hi.ten + ", " + Hi.H11 + ", " + Hi.H12 + ", " + Hi.H13 + ", " + Hi.H14 + ", " + Hi.H15 + ", " + Hi.H16 + ", " + Hi.H17 + ", " + Hi.H18 + ", " + Hi.H19 + ", " + Hi.H20  } \n<b>Добро пожаловать в наш уютный чат ABCC СНГ \nМы всегда рады тебе в нашем чате!😉\n" + "Воспользуйся нашим ботом</b> @ProAggressive_bot <b>с множеством полезных команд!\n</b><b>Последний блок</b> /last_block@ProAggressive_bot \n<b>Цена АТ</b> /at_usdt@ProAggressive_bot \n<b>Цена BTC</b> /btc_usdt@ProAggressive_bot \n<b>Цена ETH</b> /eth_usdt@ProAggressive_bot \n<b>А также множество других команд! Нажми / и действуй!</b> \n<b>Обучающий ролик!</b>" + "\n" + @" <a href=""https://www.youtube.com/watch?v=shzrEeLKZWU"">📈📈📈</a>", ParseMode.Html, false, false, 0, inlineKeyboardMarkup);

                                        //https://www.youtube.com/watch?v=shzrEeLKZWU&t=1s
                                        Hi.one = null;
                                        Hi.two = null;
                                        Hi.three = null;
                                        Hi.four = null;
                                        Hi.five = null;
                                        Hi.six = null;
                                        Hi.seven = null;
                                        Hi.eight = null;
                                        Hi.nine = null;
                                        Hi.ten = null;
                                        Hi.H11 = null;
                                        Hi.H12 = null;
                                        Hi.H13 = null;
                                        Hi.H14 = null;
                                        Hi.H15 = null;
                                        Hi.H16 = null;
                                        Hi.H17 = null;
                                        Hi.H18 = null;
                                        Hi.H19 = null;
                                        Hi.H20 = null;

                                        return;
                                    }
                                    else
                                    {
                                        string H20 = update.Message.From.FirstName + " " + update.Message.From.LastName;
                                        Hi.H20 = H20;
                                        await Bot.DeleteMessageAsync(message.Chat.Id, message.MessageId);
                                        await Bot.SendTextMessageAsync(message.Chat.Id, $"{Hi.one + ", " + Hi.two + ", " + Hi.three + ", " + Hi.four + ", " + Hi.five + ", " + Hi.six + ", " + Hi.seven + ", " + Hi.eight + ", " + Hi.nine + ", " + Hi.ten + ", " + Hi.H11 + ", " + Hi.H12 + ", " + Hi.H13 + ", " + Hi.H14 + ", " + Hi.H15 + ", " + Hi.H16 + ", " + Hi.H17 + ", " + Hi.H18 + ", " + Hi.H19 + ", " + Hi.H20  } \n<b>Добро пожаловать в наш уютный чат ABCC СНГ \nМы всегда рады тебе в нашем чате!😉\n" + "Воспользуйся нашим ботом</b> @ProAggressive_bot <b>с множеством полезных команд!\n</b><b>Последний блок</b> /last_block@ProAggressive_bot \n<b>Цена АТ</b> /at_usdt@ProAggressive_bot \n<b>Цена BTC</b> /btc_usdt@ProAggressive_bot \n<b>Цена ETH</b> /eth_usdt@ProAggressive_bot \n<b>А также множество других команд! Нажми / и действуй!</b> \n<b>Обучающий ролик!</b>" + "\n" + @" <a href=""https://www.youtube.com/watch?v=shzrEeLKZWU"">!</a>", ParseMode.Html, false, false, 0, inlineKeyboardMarkup);

                                        Hi.one = null;
                                        Hi.two = null;
                                        Hi.three = null;
                                        Hi.four = null;
                                        Hi.five = null;
                                        Hi.six = null;
                                        Hi.seven = null;
                                        Hi.eight = null;
                                        Hi.nine = null;
                                        Hi.ten = null;
                                        Hi.H11 = null;
                                        Hi.H12 = null;
                                        Hi.H13 = null;
                                        Hi.H14 = null;
                                        Hi.H15 = null;
                                        Hi.H16 = null;
                                        Hi.H17 = null;
                                        Hi.H18 = null;
                                        Hi.H19 = null;
                                        Hi.H20 = null;

                                        return;
                                    }
                                }
                                return;
                            }
                            catch
                            {

                            }

                        }

                        var entities = message.Entities.Where(t => t.Type == MessageEntityType.Url
                                           || t.Type == MessageEntityType.Mention);
                        foreach (var entity in entities)
                        {
                            if (entity.Type == MessageEntityType.Url)
                            {
                                try
                                {
                                    //40103694 - @off_fov
                                    //571522545 -  @ProAggressive                                    
                                    //320968789 - @timcheg1
                                    //273228404 - @hydranik
                                    //435567580 - Никита                           
                                    //352345393 - @i_am_zaytsev
                                    //430153320 - @KingOfMlnD
                                    //579784 - @kamiyar
                                    //536915847 - @m1Bean
                                    //460657014 - @DenisSenatorov

                                    if (message.From.Username == @"off_fov"|| message.From.Username == @"LindaMao" || message.From.Username == @"XiniW" || message.From.Username == @"ProAggressive" || message.From.Username == @"timcheg1" || message.From.Username == @"hydranik" || message.From.Username == @"i_am_zaytsev" || message.From.Username == @"KingOfMlnD" || message.From.Username == @"kamiyar" || message.From.Username == @"m1Bean" || message.From.Username == @"DenisSenatorov" || message.From.Id == 435567580)
                                    {
                                        return;
                                    }
                                    else
                                    {
                                        await Bot.DeleteMessageAsync(message.Chat.Id, message.MessageId);
                                        if (update.Message.From.Username != null)
                                        {
                                            await Bot.SendTextMessageAsync(message.Chat.Id, "@" + message.From.Username + ", Ссылки запрещены!");
                                            return;
                                        }
                                        else
                                        {
                                            await Bot.SendTextMessageAsync(message.Chat.Id, message.From.FirstName + ", Ссылки запрещены!");
                                            return;
                                        }
                                    }
                                }
                                catch
                                {

                                }
                                return;

                            }
                        }
                    }
                    catch
                    {

                    }
                };

                Bot.StartReceiving();

                // запускаем прием обновлений

            }

            catch (Telegram.Bot.Exceptions.ApiRequestException ex)
            {
                Console.WriteLine(ex.Message); // если ключ не подошел - пишем об этом в консоль отладки
            }

        }

       

        private void Form1_Load(object sender, EventArgs e)
        {
            //var text = @"737461958:AAFiybxVZYkkAC9gcly-aENm-AaLViizLX8"; // получаем содержимое текстового поля txtKey в переменную text

            // 673649420:AAG2O4s9qDmBVpmFtt4wG12dZ3nV-nBm3JA    //test test test test test 

        }


        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {

        }


        private void groupBox1_Enter_1(object sender, EventArgs e)
        {

        }

        private void txtKey_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void BtnRun_Click_1(object sender, EventArgs e)
        {
            var text = @txtKey.Text; // получаем содержимое текстового поля txtKey в переменную text
            if (text != "" && this.bw.IsBusy != true)
            {
                this.bw.RunWorkerAsync(text); // передаем эту переменную в виде аргумента методу bw_DoWork
                BtnRun.Text = "Бот запущен...";
            }
        }

        private void Form1_Load_1(object sender, EventArgs e)
        {

        }
    }
}

