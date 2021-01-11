using Terminal.Gui;
using System.Collections.Generic;
using System.Threading;
using System;
using System.Configuration;

namespace TRUCKCONSOLE
{
    class Program
    {
        //private static string _username;
        private static readonly List<string> _users = new List<string>();
        private static readonly List<string> _messages = new List<string>();

        private static void Main()
        {
        //     ReadAllSettings();
        //      static void ReadAllSettings()  
        // {  
        //     try  
        //     {  
        //         var appSettings = ConfigurationManager.AppSettings;  

        //         if (appSettings.Count == 0)  
        //         {  
        //             Console.WriteLine("AppSettings is empty.");  
        //         }  
        //         else  
        //         {  
        //             foreach (var key in appSettings.AllKeys)  
        //             {  
        //                 Console.WriteLine("Key: {0} Value: {1}", key, appSettings[key]);  
        //             }  
        //         }  
        //     }  
        //     catch (ConfigurationErrorsException)  
        //     {  
        //         Console.WriteLine("Error reading app settings");  
        //     }  
        // }  

          //******************************************************************************  
            var dbcontex = new TRUCKMODEL.Models.truckmanageContext();
            TRUCKMODEL.BEMagazzino BE = new TRUCKMODEL.BEMagazzino();
            var q = BE.GetMagazzinoView();

            foreach (var item in q)
            {
                Console.WriteLine($"ID: {item.IdMagazzino} CODICE: {item.CodiceArticolo} DSCRIZIONE: {item.NomeArticolo}");
            }
//***************************************************************************************************************
            // Application.Init();
            // var top = Application.Top;

            // var mainWindow = new Window("MAGAZZINO")
            // {
            //     X = 0,
            //     Y = 1, // Leave one row for the toplevel menu

            //     // By using Dim.Fill(), it will automatically resize without manual intervention
            //     Width = Dim.Fill(),
            //     Height = Dim.Fill()
            // };

            // #region top-menu
            // top.Add(
            //     new MenuBar(new MenuBarItem[] {
            //         new MenuBarItem("_File", new MenuItem[]{
            //             new MenuItem("_Quit", "", Application.RequestStop)
            //         }), // end of file menu
            //         new MenuBarItem("_Help", new MenuItem[]{
            //             new MenuItem("_About", "", ()
            //                         => MessageBox.Query(50, 5, "About", "Scirtto da Casali Federico\nVersion: 0.1", "Ok"))
            //         }) // end of the help menu
            //     })
            // );
            // #endregion

            // View FindViewFrame = CreaFindView();
            // mainWindow.Add(FindViewFrame);

            // View Details_View = CreaDetailsView(4);
            // mainWindow.Add(Details_View);




            // #region Grid_view
            // var Grid_View = new FrameView()
            // {
            //     X = 0,
            //     Y = Pos.Bottom(Details_View),
            //     Width = FindViewFrame.Width,
            //     Height = Dim.Fill()
            // };
            // #endregion





            // mainWindow.Add(Grid_View);



            // top.Add(mainWindow);
            // Application.Run();
        }
       private static List<string> CaricaCategoria()
       {
           var MyLista = new List<string>();
           try
           {
            TRUCKMODEL.BEMagazzino be = new TRUCKMODEL.BEMagazzino();
            var Query = be.GetMagCategoria();

            foreach (var item in Query)
            {
                MyLista.Add(item.Categoria);
            }
           }
           catch
           {}


           return MyLista;
       }

        private static View CreaFindView()
        {
            var FindViewFrame = new FrameView("Ricerca")
            {
                X = 0,
                Y = 1,
                Width = Dim.Fill(),
                Height = Dim.Sized(3),
            };


            var LblFindCategoria = new Label("Categoria:")
            {
                X = 0,
                Y = 0,
                Width = Dim.Sized(11),
                Height = Dim.Fill()
            };

            var CmbFindCategoria = new ComboBox()
            {
                X = Pos.Right(LblFindCategoria),
                Y = 0,
                Width = Dim.Sized(40),
                Height = Dim.Fill()
            };

            //sorgente di esempio altrimenti esplode quando in run se vuoto
            CmbFindCategoria.SetSource(CaricaCategoria());
           // CmbFindCategoria.OpenSelectedItem += (ListViewItemEventArgs text) => { Application.RequestStop(); };

            var LblFindCodice = new Label(" Codice Articolo:")
            {
                X = Pos.Right(CmbFindCategoria),
                Y = 0,
                //17 e il numero di caratteri per scrivere 'Codice articolo'
                Width = Dim.Sized(17),
                Height = Dim.Fill()
            };

            var FindCodiceArticolo = new TextField("")
            {
                X = Pos.Right(LblFindCodice),
                Y = 0,
                Width = Dim.Sized(40),
                Height = Dim.Fill()
            };


            var LblFindDescrizione = new Label(" Descrizione:")
            {
                X = Pos.Right(FindCodiceArticolo),
                Y = 0,
                //17 e il numero di caratteri per scrivere 'Codice articolo'
                Width = Dim.Sized(13),
                Height = Dim.Fill()
            };

            var FindDescrizione = new TextField("")
            {
                X = Pos.Right(LblFindDescrizione),
                Y = 0,
                Width = Dim.Sized(50),
                Height = Dim.Fill()
            };

            FindViewFrame.Add(LblFindCategoria);
            FindViewFrame.Add(CmbFindCategoria);
            FindViewFrame.Add(LblFindCodice);
            FindViewFrame.Add(FindCodiceArticolo);
            FindViewFrame.Add(LblFindDescrizione);
            FindViewFrame.Add(FindDescrizione);

            return FindViewFrame;
        }

        private static View CreaDetailsView(int posY)
        {
            var Details_View = new FrameView("Details")
            {
                X = 0,
                Y = posY,
                Width = Dim.Fill(),
                Height = 15
            };

            var LblId = new Label("ID:")
            { X = 0, Y = 0, Width = Dim.Sized(4), Height = Dim.Fill() };

            var TxtId = new TextField("")
            {
                X = Pos.Right(LblId),
                Y = 0,
                Width = Dim.Sized(10),
                Height = Dim.Fill(),
                ReadOnly = true
            };

            var LblDescrizione = new Label(" Descrizione:")
            { X = Pos.Right(TxtId), Y = 0, Width = Dim.Sized(13), Height = Dim.Fill() };

            var TxtDescrizione = new TextField("")
            {
                X = Pos.Right(LblDescrizione),
                Y = 0,
                Width = Dim.Sized(70),
                Height = Dim.Fill()
            };

            var LblMarca = new Label(" Marca:")
            { X = Pos.Right(TxtDescrizione), Y = 0, Width = Dim.Sized(7), Height = Dim.Fill() };

            var TxtMarca = new TextField("")
            {
                X = Pos.Right(LblMarca),
                Y = 0,
                Width = Dim.Sized(40),
                Height = Dim.Fill()
            };

            var LblUnita = new Label("Unita Misura:")
            { X = 0, Y = 2, Width = Dim.Sized(13), Height = Dim.Fill() };

            var TxtUnita = new TextField("")
            {
                X = Pos.Right(LblUnita),
                Y = 2,
                Width = Dim.Sized(10),
                Height = Dim.Fill()
            };

            var LblGiacenza = new Label(" Giacenza:")
            { X = Pos.Right(TxtUnita), Y = 2, Width = Dim.Sized(10), Height = Dim.Fill() };

            var TxtGiacenza = new TextField("")
            {
                X = Pos.Right(LblGiacenza),
                Y = 2,
                Width = Dim.Sized(5),
                Height = Dim.Fill()
            };


            var LblGiacenzaMin = new Label(" Giacenza MIN:")
            { X = Pos.Right(TxtGiacenza), Y = 2, Width = Dim.Sized(14), Height = Dim.Fill() };

            var TxtGiacenzaMin = new TextField("")
            {
                X = Pos.Right(LblGiacenzaMin),
                Y = 2,
                Width = Dim.Sized(5),
                Height = Dim.Fill()
            };

            var LblPrezzoUni = new Label(" Prezzo Unitario(€):")
            { X = Pos.Right(TxtGiacenzaMin), Y = 2, Width = Dim.Sized(20), Height = Dim.Fill() };

            var TxtPrezzoUni = new TextField("")
            {
                X = Pos.Right(LblPrezzoUni),
                Y = 2,
                Width = Dim.Sized(10),
                Height = Dim.Fill()
            };

            var LblIva = new Label(" IVA:")
            { X = Pos.Right(TxtPrezzoUni), Y = 2, Width = Dim.Sized(5), Height = Dim.Fill() };

            var TxtIva = new TextField("")
            {
                X = Pos.Right(LblIva),
                Y = 2,
                Width = Dim.Sized(5),
                Height = Dim.Fill()
            };

            var LblPrezzoTot = new Label(" PrezzoTot:")
            { X = Pos.Right(TxtIva), Y = 2, Width = Dim.Sized(11), Height = Dim.Fill() };

            var TxtPrezzoTot = new TextField("")
            {
                X = Pos.Right(LblPrezzoTot),
                Y = 2,
                Width = Dim.Sized(10),
                Height = Dim.Fill()
            };


            var LblFornitore = new Label(" Fornitore:")
            { X = Pos.Right(TxtPrezzoTot), Y = 2, Width = Dim.Sized(11), Height = Dim.Fill() };

            var TxtFornitore = new TextField("")
            {
                X = Pos.Right(LblFornitore),
                Y = 2,
                Width = Dim.Sized(40),
                Height = Dim.Fill()
            };

            var LblNote = new Label("Note:")
            { X = 0, Y = 4, Width = Dim.Sized(5), Height = Dim.Fill() };

            var TxtNote = new TextView()
            {
                X = Pos.Right(LblNote),
                Y = 4,
                Width = 150,
                Height = 10
            };

            // var sendButton = new Button("Send", true)
            // {
            //     X = Pos.Right(chatMessage),
            //     Y = 0,
            //     Width = Dim.Fill(),
            //     Height = Dim.Fill()
            // };

            // sendButton.Clicked += () =>
            // {
            //     Application.MainLoop.Invoke(() =>
            //     {
            //         // _messages.Add($"{_username}: {chatMessage.Text}");
            //         // chatView.SetSource(_messages);
            //         // chatMessage.Text = "";
            //     });
            // };

            Details_View.Add(LblId);
            Details_View.Add(TxtId);
            Details_View.Add(LblDescrizione);
            Details_View.Add(TxtDescrizione);
            Details_View.Add(LblMarca);
            Details_View.Add(TxtMarca);
            Details_View.Add(LblUnita);
            Details_View.Add(TxtUnita);
            Details_View.Add(LblGiacenza);
            Details_View.Add(TxtGiacenza);
            Details_View.Add(LblGiacenzaMin);
            Details_View.Add(TxtGiacenzaMin);
            Details_View.Add(LblPrezzoUni);
            Details_View.Add(TxtPrezzoUni);
            Details_View.Add(LblIva);
            Details_View.Add(TxtIva);
            Details_View.Add(LblPrezzoTot);
            Details_View.Add(TxtPrezzoTot);
            Details_View.Add(LblFornitore);
            Details_View.Add(TxtFornitore);

            Details_View.Add(LblNote);
            Details_View.Add(TxtNote);

TxtDescrizione.Text ="CAZZONE";

            return Details_View;

        }
    }

}

