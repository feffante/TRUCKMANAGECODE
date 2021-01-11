using System;
using System.Collections;
using System.Data;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace UtilityBE
{
    /// <summary>
    /// Utility locali.
    /// </summary>
    [Serializable]
    public class Utility
    {
        /// <summary>
        /// Costruttore vuoto.
        /// </summary>
        public Utility() { }

        //
        // Funzione di log, il primo parametro e' il testo da loggare
        // il secondo parametro e' il file di log
        public void Log(string LogText, string LogFileName)
        {
            try
            {
                FileStream LogFileStream = new FileStream(LogFileName, FileMode.OpenOrCreate, FileAccess.Write);
                StreamWriter LogFileStreamWriter = new StreamWriter(LogFileStream);
                LogFileStream.Seek(0, SeekOrigin.End);
                LogFileStreamWriter.WriteLine(LogText);
                LogFileStreamWriter.Flush();
                LogFileStreamWriter.Close();
                LogFileStream.Close();
            }
            catch
            {
                return;
            }
        }

        //
        // Funzione di log, il primo parametro e' il testo da loggare
        public void Log(string LogText)
        {
            try
            {
                FileStream LogFileStream = new FileStream("c:\\logWebPortal.log", FileMode.OpenOrCreate, FileAccess.Write);
                StreamWriter LogFileStreamWriter = new StreamWriter(LogFileStream);
                LogFileStream.Seek(0, SeekOrigin.End);
                LogFileStreamWriter.WriteLine(LogText);
                LogFileStreamWriter.Flush();
                LogFileStreamWriter.Close();
                LogFileStream.Close();
            }
            catch
            {
                return;
            }
        }

        /// <summary>
        ///		Lancia una applicazione shell e rimane in attesa che essa termini l'esecuzione.
        /// </summary>
        /// <param name="PathName">Pathname dell'applicazione da lanciare (es. c:\openssl\bin.openssl.exe).</param>
        /// <param name="Args">Argomenti passati all'eseguibile.</param>
        /// <param name="UseShellExecute">Indica se usare la shell o creare il prcesso direttamente dall'eseguibile.</param>
        /// <returns>Exit code dell'applicazione.</returns>
        public int SpawnApplication(string PathName, string Args, bool UseShellExecute)
        {
            int exitcode = 0;
            try
            {
                System.Diagnostics.Process prc = new System.Diagnostics.Process();
                prc.StartInfo.FileName = PathName;
                prc.StartInfo.Arguments = Args;
                prc.StartInfo.UseShellExecute = UseShellExecute;
                prc.EnableRaisingEvents = true;
                prc.Start();
                prc.WaitForExit();
                exitcode = prc.ExitCode;
                //prc.Dispose(); 
            }
            catch (Exception e)
            {
                exitcode = short.MinValue;
                throw (e);
            }
            return exitcode;
        }

        //
        // Restituisce il nome della macchina su cui 
        // sta girando il processo che invoca questo metodo
        public String getComputerName()
        {
            return (String)System.Environment.MachineName;
        }


        /// <summary>
        /// Funzione che restituisce il nome del file nella tipologia richiesta.
        /// </summary>
        /// <param name="sNamefileOrig"> Nome comprensivo di path </param>
        /// <param name="iFileNameType"> Parametro che identifica (0 -> trancia estensione , 1 -> restituisce nome, 2 -> restituisce estensione)</param>
        /// <returns>
        /// Stringa contenente il nome del file nella tipologia richiesta.
        /// </returns>
        /// <remarks>
        /// <newpara>
        /// Richiama il metodo "GetfileName" e, a secona del valore di iFileNameType, compone la strina di ritorno:
        /// 0 -> restituisce nome;
        /// 1 -> restituisce nome ed estensione;
        /// 2 -> restituisce estensione.
        /// </newpara>
        /// </remarks>
        public static string GetfileName(string sNamefileOrig, int iFileNameType)
        {
            int nIndex = 0;
            string sNomeFile = "";
            string result = "";

            sNomeFile = GetfileName(sNamefileOrig);
            if (iFileNameType == 2)
                result = "";
            else
                result = sNomeFile;

            for (nIndex = sNomeFile.Length; nIndex == 0; nIndex--)
            {
                if (sNomeFile.Substring(nIndex, 1) == @".")
                {
                    if (iFileNameType == 2)
                        result = sNomeFile.Substring(nIndex + 1);
                    else
                        result = sNomeFile.Substring(0, nIndex - 1);
                    nIndex = 0;
                }
            }
            return result;
        }

        /// <summary>
        /// Funzione che restituisce il nome_file.estensione (esclude il path)
        /// </summary>
        /// <param name="sNamefile"> Nome comprensivo di path  </param>
        private static string GetfileName(string sNamefile)
        {
            int nIndex = 0;
            string result = sNamefile;

            for (nIndex = sNamefile.Length - 1; nIndex >= 0; nIndex--)
            {

                if (sNamefile.Substring(nIndex, 1) == @"\")
                {
                    result = sNamefile.Substring(nIndex + 1);
                    nIndex = -1;
                }
            }
            return result;
        }

        public static string ExportByteArrayToString(byte[] bArray)
        {
            UTF8Encoding encoding = new UTF8Encoding();
            Decoder decoder = encoding.GetDecoder();
            int iByteToDecode = decoder.GetCharCount(bArray, 0, bArray.Length);
            Array chars = Array.CreateInstance(typeof(char), iByteToDecode);
            decoder.GetChars(bArray, 0, iByteToDecode, (char[])chars, 0);

            return new String((char[])chars);
        }

        private void ExportToExcelAndCsv()
        {
            //dgdisplay.selectallcells();
            //dgdisplay.clipboardcopymode = datagridclipboardcopymode.includeheader;
            //applicationcommands.copy.execute(null, dgdisplay);
            //string resultat = (string)clipboard.getdata(dataformats.commaseparatedvalue);
            //string result = (string)clipboard.getdata(dataformats.text);
            //dgdisplay.unselectallcells();
            //system.io.streamwriter file1 = new system.io.streamwriter(@"c:\users\test.xls");
            //file1.writeline(result.replace(',', ' '));
            //file1.close();

            //messagebox.show(" Exporting DataGrid data to Excel file created.xls");
        }

        /// <summary>
        /// Metodo per Scompattare una tabella di un DataSet in una String.
        /// </summary>
        /// <param name="dsToConvert">DataSet da convertire</param>
        /// <param name="iDataTable">Identificativo tabella da convertire</param>
        /// <param name="cRowDelimiter">Separatore di RIGA</param>
        /// <param name="cColumnDelimiter">Separatore di COLONNA</param>
        /// <param name="cColumnNameDelimiter">Separatore tra il nome di colonna e il valore</param>
        public string ExportDataSetToStringone(DataSet dsToConvert, int iDataTable, char cRowDelimiter, char cColumnDelimiter, char cColumnNameDelimiter)
        {
            // Inizializzo SB
            StringBuilder sbConverted = new StringBuilder();

            // Scorro tutte le righe della tabella
            for (int i = 0; i < dsToConvert.Tables[iDataTable].Rows.Count; i++)
            {
                // Scorro tutte le colonne della riga della tabella
                for (int j = 0; j < dsToConvert.Tables[iDataTable].Columns.Count; j++)
                {
                    // Scrivo NOME COLONNA + delimitatore nome + VALORE + delimitatore colonna
                    sbConverted.Append(dsToConvert.Tables[iDataTable].Columns[j].ColumnName + cColumnNameDelimiter + dsToConvert.Tables[iDataTable].Rows[i][j].ToString() + cColumnDelimiter);
                }

                // Scrivo delimitatore riga
                sbConverted.Append(cRowDelimiter);
            }

            // Ritorno SB
            return sbConverted.ToString();
        }

        /// <summary>
        /// Metodo per Scompattare una tabella di un DataSet in una String.
        /// </summary>
        /// <param name="dsToConvert">DataSet da convertire</param>
        /// <param name="iDataTable">Identificativo tabella da convertire</param>
        /// <param name="cRowDelimiter">Separatore di RIGA</param>
        /// <param name="cColumnDelimiter">Separatore di COLONNA</param>
        public string ExportDataSetToStringone(DataSet dsToConvert, int iDataTable, char cRowDelimiter, char cColumnDelimiter)
        {
            // Inizializzo SB
            StringBuilder sbConverted = new StringBuilder();

            // Scorro tutte le righe della tabella
            for (int i = 0; i < dsToConvert.Tables[iDataTable].Rows.Count; i++)
            {
                // Scorro tutte le colonne della riga della tabella
                for (int j = 0; j < dsToConvert.Tables[iDataTable].Columns.Count; j++)
                {
                    // Scrivo VALORE + delimitatore colonna 
                    sbConverted.Append(dsToConvert.Tables[iDataTable].Rows[i][j].ToString() + cColumnDelimiter);
                }

                // Scrivo delimitatore riga 
                sbConverted.Append(cRowDelimiter);
            }

            // Ritorno SB
            return sbConverted.ToString();
        }
        /// <summary>
        /// Metodo per Scompattare una tabella di un DataSet in una String.
        /// </summary>
        /// <param name="dsToConvert">DataSet da convertire</param>
        /// <param name="iDataTable">Identificativo tabella da convertire</param>
        /// <param name="cRowDelimiter">(char/string) Separatore di RIGA, se null non viene inserito nulla</param>
        /// <param name="cColumnDelimiter">(char/string) Separatore di COLONNA, se null non viene inserito nulla</param>
        public string ExportDataSetToStringone(DataSet dsToConvert, int iDataTable, object cRowDelimiter, object cColumnDelimiter)
        {
            // Inizializzo SB
            StringBuilder sbConverted = new StringBuilder();

            // Scorro tutte le righe della tabella
            for (int i = 0; i < dsToConvert.Tables[iDataTable].Rows.Count; i++)
            {
                // Scorro tutte le colonne della riga della tabella
                for (int j = 0; j < dsToConvert.Tables[iDataTable].Columns.Count; j++)
                {
                    // Scrivo VALORE + delimitatore colonna (se quest'ultimo non � null)
                    if (cColumnDelimiter == null)
                        sbConverted.Append(dsToConvert.Tables[iDataTable].Rows[i][j].ToString());
                    else
                        sbConverted.Append(dsToConvert.Tables[iDataTable].Rows[i][j].ToString() + cColumnDelimiter.ToString());
                }

                // Scrivo delimitatore riga (se quest'ultimo non � null)
                if (cRowDelimiter != null)
                    sbConverted.Append(cRowDelimiter.ToString());
            }

            // Ritorno SB
            return sbConverted.ToString();
        }

        /// <summary>
        /// Metodo per Scompattare una colonna di una tabella di un DataSet in una String.
        /// </summary>
        /// <param name="dsToConvert">DataSet da convertire</param>
        /// <param name="iDataTable">Identificativo tabella da convertire</param>
        /// <param name="iDataColumn">Identificativo colonna da convertire</param>
        /// <param name="cRowDelimiter">(char/string) Separatore di RIGA, se null non viene inserito nulla</param>
        /// <param name="cColumnDelimiter">(char/string) Separatore di COLONNA, se null non viene inserito nulla</param>
        public string ExportDataSetToStringone(DataSet dsToConvert, int iDataTable, int iDataColumn, object cRowDelimiter)
        {
            // Inizializzo SB
            StringBuilder sbConverted = new StringBuilder();

            // Scorro tutte le righe della tabella
            for (int i = 0; i < dsToConvert.Tables[iDataTable].Rows.Count; i++)
            {
                sbConverted.Append(dsToConvert.Tables[iDataTable].Rows[i][iDataColumn].ToString());
                if (cRowDelimiter != null)
                    sbConverted.Append(cRowDelimiter.ToString());
            }

            // Ritorno SB
            return sbConverted.ToString();
        }


        public string Base36(int iNum)
        {
            string sStringaBase = "0123456789ABCDEFGHILMNOPQRSTUVZXYJKW";
            int iBase = 36;
            string sReturn = "";
            int iCount = 0;
            int iResto = iNum;
            int iIntero = iNum;
            while (iResto >= 0)
            {
                iCount += 1;
                if (iIntero >= iBase)
                {
                    iIntero = iNum / iBase;
                    iResto = iNum - (iIntero * iBase);
                    iNum = iIntero;
                }
                else
                {
                    sReturn = sStringaBase.Substring(iIntero, 1) + sReturn;
                    break;
                }
                //sReturn = sStringaBase.Substring(iResto +1,1) + sReturn;
                sReturn = sStringaBase.Substring(iResto, 1) + sReturn;
            }

            return sReturn;
        }


        public string ConvertToBase64String(string input)
        {
            byte[] info = Encoding.Unicode.GetBytes(input);

            // Converte l�input binario in output Base64 UUEncoded.
            // Ciascuna sequenza di 3 byte nei dati di partenza diventa
            // una sequenza di 4 byte nell�array di caratteri.

            long dataLength = (long)Math.Round(1.3333333333333333 * info.Length);

            // Se la lunghezza non � divisibile per 4, si arrotonda al multiplo successivo
            // di 4.
            if (dataLength % 4 != 0)
                dataLength = dataLength + 4 - dataLength % 4;


            // Alloca il buffer di output
            char[] base64CharArray = new char[(int)dataLength + 1];

            // Converte...
            Convert.ToBase64CharArray(info, 0, info.Length, base64CharArray, 0);

            // Visualizza i dati convertiti
            return new String(base64CharArray);
        }


        public string LoadFileToString(string filename)
        {
            try
            {
                using (StreamReader sr = new StreamReader(filename))
                    return sr.ReadToEnd().Trim();
            }
            catch (Exception ex)
            {
                AppLogger.Error("LoadFileToString", "filename=" + filename);
                throw (ex);
            }
        }


        public StringBuilder LoadFileToStringBuilder(string filename, string fineriga)
        {
            StringBuilder data = new StringBuilder();

            try
            {
                using (StreamReader sr = new StreamReader(filename))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                        data.Append(line + fineriga);
                }
            }

            finally
            {
                data = null;
            }

            return data;
        }


        public void SaveFileFromString(string filename, string data)
        {
            string path = filename.Substring(0, filename.LastIndexOf("\\"));
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            using (StreamWriter sw = new StreamWriter(filename))
                sw.Write(data);
        }


        public byte[] LoadBinaryFile(string filename)
        {
            FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read);
            byte[] data = new byte[fs.Length];
            fs.Read(data, 0, (int)fs.Length);
            fs.Close();
            return data;
        }
        public void SaveBinaryFile(string filename, byte[] data)
        {
            string path = filename.Substring(0, filename.LastIndexOf("\\"));
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            FileStream fs = new FileStream(filename, FileMode.Create, FileAccess.Write);
            fs.Write(data, 0, data.Length);
            fs.Close();
        }

        public string[] CopyDataToStrings(byte[] data)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i <= data.GetUpperBound(0); i++)
                if (((char)data[i]) != '\r') sb.Append((char)data[i]);
            return sb.ToString().Split('\n');
        }

        public string[] LoadFileToStringArray(string filename)
        {
            ArrayList data = new ArrayList();
            using (StreamReader sr = new StreamReader(filename))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                    data.Add(line);
            }
            string[] sdata = new string[data.Count];
            for (int i = 0; i <= data.Count - 1; i++)
                sdata[i] = data[i].ToString();
            return sdata;
        }
        public void SaveFileFromStringArray(string filename, string[] data)
        {
            string path = filename.Substring(0, filename.LastIndexOf("\\"));
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            using (StreamWriter sw = new StreamWriter(filename))
                for (int i = data.GetLowerBound(0); i <= data.GetUpperBound(0); i++)
                    sw.WriteLine(data[i]);
        }

        public byte[] GetDataHash(string HashAlgorithm, byte[] DataToHash)
        { return ((HashAlgorithm)CryptoConfig.CreateFromName(HashAlgorithm)).ComputeHash(DataToHash); }
        public byte[] GetFileHash(string HashAlgorithm, string filename)
        { return GetDataHash(HashAlgorithm, LoadBinaryFile(filename)); }

        public bool IsEquals(byte[] a, byte[] b)
        {
            bool IsEql = (a.GetLowerBound(0) == b.GetLowerBound(0)) && (a.GetUpperBound(0) == b.GetUpperBound(0));
            int i = a.GetLowerBound(0);
            while (IsEql && (i <= a.GetUpperBound(0)))
            {
                IsEql = (a[i] == b[i]);
                i++;
            }
            return IsEql;
        }


        public void MakeHashFile(string HashAlgorithm, string DataFilename, string HashFilename)
        { SaveBinaryFile(HashFilename, GetFileHash(HashAlgorithm, DataFilename)); }
        public bool VerifyHashFile(string HashAlgorithm, string DataFilename, string HashFilename)
        { return IsEquals(GetFileHash(HashAlgorithm, DataFilename), LoadBinaryFile(HashFilename)); }


        public string GenerateRandomString120(int nrows, int p)
        {
            StringBuilder result = new StringBuilder();
            Random rnd = new Random(); int c;
            for (int i = 0; i <= nrows; i++)
            {
                for (int j = 0; j <= 119; j++)
                {
                    if (rnd.Next(100) < p)
                    {
                        c = rnd.Next(62);
                        if (c <= 25)
                            result.Append((char)(65 + c));
                        else
                            if (26 <= c && c <= 51)
                            result.Append((char)(96 + c - 26));
                        else
                            result.Append((char)(48 + c - 52));
                    }
                    else
                        result.Append(" ");
                }
                result.Append("\r\n");
            }
            return result.ToString();
        }


        public static String base36(int iNum)
        {

            String STRINGA_BASE36 = "0123456789ABCDEFGHILMNOPQRSTUVZXYJKW";
            String cStringa = STRINGA_BASE36;
            String cTemp = "";
            int nResto;
            int nIntero;
            int nBase = 36;
            int I = 0;

            nResto = iNum;
            nIntero = iNum;

            while (nResto >= 0)
            {
                I = I + 1;
                if (nIntero >= nBase)
                {
                    nIntero = iNum / nBase;
                    nResto = iNum - (nIntero * nBase);
                    iNum = nIntero;
                }
                else
                {
                    //cTemp = cStringa.Substring((nIntero + 1), 1) + cTemp;
                    cTemp = cStringa.Substring(nIntero, 1) + cTemp;
                    break;
                }
                //cTemp = cStringa.Substring(nResto + 1, 1) + cTemp;
                cTemp = cStringa.Substring(nResto, 1) + cTemp;
            }
            return cTemp;

        }


        /// <summary>
        /// Esporta un dataset in una stringa in formato XML.
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        public string ExportDataSetToXmlString(DataSet ds)
        {
            for (int t = 0; t < ds.Tables.Count; t++)
                for (int c = 0; c < ds.Tables[t].Columns.Count; c++)
                    ds.Tables[t].Columns[c].ColumnMapping = MappingType.Attribute;
            MemoryStream MemStream = new MemoryStream();
            ds.WriteXml(MemStream);
            // string s = Utility.ExportByteArrayToString(MemStream.ToArray());
            byte[] ba = MemStream.ToArray();
            return ASCIIEncoding.UTF8.GetString(ba);

            //			StringBuilder sb = new StringBuilder(ba.Length);
            //			for( int i=0; i<ba.Length; i++ ) sb.Append((char)ba[i]);
            //			return sb.ToString();
        }


        public byte[] ConvertHex2Byte(string s)
        {
            long size = s.Length / 2;
            byte[] r = new byte[size];
            for (int i = 0; i < size; i++)
                r[i] = byte.Parse((s.Substring(i * 2, 2)), System.Globalization.NumberStyles.AllowHexSpecifier);
            return r;
        }

        public string ConvertByte2Hex(byte[] b)
        {
            StringBuilder sb = new StringBuilder(b.Length);
            for (int i = 0; i < b.Length; i++)
                sb.Append(((int)b[i]).ToString("X2"));
            return sb.ToString();
        }



        /// <summary>
        /// Funzione che verifica se la stringa inserita � un numero o meno.
        /// </summary>
        /// <param name="s">Valore da parserizzare.</param>
        /// <returns>Vero o falso a seconda che s sia un numero o meno.</returns>
        public bool IsNumeric(string s)
        {
            try
            {
                int.Parse(s);
            }
            catch
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Funzione che verifica se la stringa inserita � un double o meno.
        /// </summary>
        /// <param name="s">Valore da parserizzare.</param>
        /// <returns>Vero o falso a seconda che s sia un numero o meno.</returns>
        public bool IsDouble(string s)
        {
            try
            {
                double.Parse(s);
            }
            catch
            {
                return false;
            }
            return true;
        }


        /// <summary>
        /// Import della funzione di Encode (da "Encoder.dll").
        /// </summary>
        [DllImport("Encoder.dll")]
        public static extern bool EncodeFile(String sFileTmp, String sFileCrypt);

        public bool EncodeFileToFile(
            string sFileTmp
            , string sFileCrypt
            )
        {
            return EncodeFile(sFileTmp, sFileCrypt);
        }

        /// <summary>
        /// Estrae da un file un array di byte.
        /// </summary>
        /// <param name="sFileName">Nome completo del file (con path)</param>
        /// <returns>
        /// rray di byte contente il contenuto del file (array binario)
        /// </returns>
        /// <remarks>
        /// <newpara>
        /// Apre il file, lo legge e lo scrive in un array di byte. 
        /// Sbotta in caso d'errore, tanto l'errore dev'essere catchato dal FrontEnd.
        /// </newpara>
        /// </remarks>
        public byte[] ExportFileToByteArray(string sFileName)
        {
            // Inizializzo array di byte 
            byte[] arrayb = null;

            // Uso try per errore applicativo
            try
            {
                // Logging accesso alla classe
                AppLogger.Info("UTENTE_APPLICATIVO", "Richiamo metodo ExportFileToByteArray, classe RemoteUtility", "Nome file ricercato: " + sFileName.ToString(), "");

                // Apro il file, ne calcolo la lunghezza e ridimensiono l'array
                Stream fs = File.OpenRead(sFileName);
                int length = (int)(new FileInfo(sFileName)).Length;
                arrayb = new byte[length];

                // Scrivo il file nalle'array di byte 
                fs.Read(arrayb, 0, length);

                // Chiudo il file
                fs.Close();
            }
            catch (Exception ex)
            {
                // In caso di eccezione, la scrivo sul log
                AppLogger.Error("UTENTE_APPLICATIVO", "Errore metodo ExportFileToByteArray, classe RemoteUtility", "Nome file ricercato: " + sFileName.ToString(), ex.Message);
                AppLogger.Error("UTENTE_APPLICATIVO", "Errore metodo ExportFileToByteArray, classe RemoteUtility", "Nome file ricercato: " + sFileName.ToString(), ex.StackTrace);
                // Ritorno eccezione
                throw (ex);
            }

            // Ritorno array di byte 
            return arrayb;
        }

        /// <summary>
        /// Estrae da un file un array di byte, partendo da una posizione specifica 
        /// nel file di input.
        /// </summary>
        /// <param name="sFileName">Nome completo del file (con path)</param>
        /// <param name="iOffStart">OffSet di partenza lettura</param>
        /// <param name="iLenght">Numero di byte da leggere</param>
        /// <returns>
        /// Array di byte contente il contenuto del file (array binario).
        /// </returns>
        /// <remarks>
        /// <newpara>
        /// Apre il file, lo legge e lo scrive in un array di byte. 
        /// Sbotta in caso d'errore, tanto l'errore dev'essere catchato dal FrontEnd.
        /// </newpara>
        /// </remarks>
        public byte[] ExportMultiFileToByteArray(string sFileName, int iOffStart, int iLenght)
        {
            byte[] arrayb = null;

            Stream fs = File.OpenRead(sFileName);
            arrayb = new byte[iLenght];

            // Scorro il file dalla posizione specifica
            fs.Position = iOffStart;
            for (int i = 0; i < iLenght; i++)
                arrayb[i] = (byte)fs.ReadByte();

            return arrayb;
        }

        /// <summary>
        /// Importa dentro il FileSystem (in sFileName), partendo da un array di byte.
        /// </summary>
        /// <param name="bArray">Array di byte (contenenuto del file)</param>
        /// <param name="sFileName">Nome completo del file (con path)</param>
        /// <returns>
        /// Intero (0 -> Tutto OK)
        /// </returns>
        /// <remarks>
        /// <newpara>
        /// Crea il file, vi scrive l'array di byte e lo chiude.
        /// Se tutto OK, ritorna 0.
        /// Sbotta in caso d'errore, tanto l'errore dev'essere catchato dal FrontEnd.
        /// </newpara>
        /// </remarks>
        public int ImportByteArrayToFile(byte[] bArray, string sFileName)
        {
            Stream sw = File.Create(sFileName);
            sw.Write(bArray, 0, bArray.Length);
            sw.Flush();
            sw.Close();

            return 0;
        }

        public int ImportByteArrayToFile(String ContentFile, string sFileName)
        {
            byte[] bArray = null;
            System.Text.UnicodeEncoding encoder = new System.Text.UnicodeEncoding();
            bArray = encoder.GetBytes(ContentFile);
            return ImportByteArrayToFile(bArray, sFileName);
        }

        /// <summary>
        /// Elimina un file (sFileName)dal FileSystem.
        /// </summary>
        /// <param name="sFileName">Nome completo del file (con path)</param>
        /// <returns>
        /// Intero (1 -> Tutto OK, equivale al numero di file cancellati).
        /// </returns>
        /// <remarks>
        /// <newpara>
        /// Elimina il file.
        /// Se tutto OK, ritorna 1.
        /// Sbotta in caso d'errore, tanto l'errore dev'essere catchato dal FrontEnd.
        /// </newpara>
        /// </remarks>
        public int DeleteFile(string sFileName)
        {
            File.Delete(sFileName);
            return 1;
        }

        /// <summary>
        /// Esegue la move di un file (sFileNameOld -> sFileNameNew) dal FileSystem.
        /// </summary>
        /// <param name="sFileNameOld">Nome completo del file (VECCHIO)</param>
        /// <param name="sFileNameNew">Nome completo del file (NUOVO)</param>
        /// <returns>
        /// Intero (1 -> Tutto OK, equivale al numero di file mossi).
        /// </returns>
        /// <remarks>
        /// <newpara>
        /// Muove il file.
        /// Se tutto OK, ritorna 1.
        /// Sbotta in caso d'errore, tanto l'errore dev'essere catchato dal FrontEnd.
        /// </newpara>
        /// </remarks>
        public int MoveFile(string sFileNameOld, string sFileNameNew)
        {
            File.Move(sFileNameOld, sFileNameNew);
            return 1;
        }

        public int UpdateOrCreateFile(String ContentFile, string sFileName)
        {
            byte[] bArray = null;
            System.Text.ASCIIEncoding encoder = new System.Text.ASCIIEncoding();
            bArray = encoder.GetBytes(ContentFile);

            if (File.Exists(sFileName))
            {
                try
                {
                    File.Copy(sFileName, sFileName + ".buffer", true);
                }
                catch (Exception ex)
                {
                    AppLogger.Error("Errore nella copia del file nella UpdateOrCreateFile Message=" + ex.Message);
                    AppLogger.Error("Errore nella copia del file nella UpdateOrCreateFile StackTrace=" + ex.StackTrace);
                    return -1;
                }

                // Cancello il file esistente solo se � andata a buon fine la copia
                DeleteFile(sFileName);
                // Salvo il file
                ImportByteArrayToFile(bArray, sFileName);
                // Cancello il file esistente
                DeleteFile(sFileName + ".buffer");
                return 0;
            }
            else
            {
                // Salvo il file
                return ImportByteArrayToFile(bArray, sFileName);
            }
        }

        public int UpdateOrCreateFile(byte[] bArray, string sFileName)
        {
            if (File.Exists(sFileName))
            {
                try
                {
                    File.Copy(sFileName, sFileName + ".buffer", true);
                }
                catch (Exception ex)
                {
                    AppLogger.Error("Errore nella copia del file nella UpdateOrCreateFile Message=" + ex.Message);
                    AppLogger.Error("Errore nella copia del file nella UpdateOrCreateFile StackTrace=" + ex.StackTrace);
                    return -1;
                }

                // Cancello il file esistente solo se � andata a buon fine la copia
                DeleteFile(sFileName);
                // Salvo il file
                ImportByteArrayToFile(bArray, sFileName);
                // Cancello il file esistente
                DeleteFile(sFileName + ".buffer");
                return 0;
            }
            else
            {
                // Salvo il file
                return ImportByteArrayToFile(bArray, sFileName);
            }
        }


        //public void SaveFileFromString(string filename, string data)
        //{
        //    string path = filename.Substring(0, filename.LastIndexOf("\\"));
        //    if (!Directory.Exists(path))
        //        Directory.CreateDirectory(path);
        //    using (StreamWriter sw = new StreamWriter(filename))
        //        sw.Write(data);
        //}

        //public void SaveBinaryFile(string filename, byte[] data)
        //{
        //    (new Utility()).SaveBinaryFile(filename, data);
        //}

        public String GetFileName(String PathComplete)
        {
            FileInfo fi = new FileInfo(PathComplete);
            return fi.Name;
        }

        public bool FileExist(String PathComplete)
        {
            return File.Exists(PathComplete);
        }

        public String GetFileExtension(String PathComplete)
        {
            FileInfo fi = new FileInfo(PathComplete);
            return fi.Extension;
        }


        public bool CreateDirectory(String PathComplete)
        {
            if (!Directory.Exists(PathComplete)) //Verifica L'esistenza della directrory se non c'e' la  creo
            {
                Directory.CreateDirectory(PathComplete);
                return true;

            }
            else
            {
                return false;
            }

        }


        /// <summary>
        /// Overload del metodo per scrivere un file nel backend: tronco l'array eliminando
        /// l'ultimo elemento dato che viene aggiunto in pi� con la trasformazione binaria
        /// </summary>
        /// <param name="bArray">array di byte contenente il file da scrivere</param>
        /// <param name="sFileName">nome del file da scrivere</param>
        /// <returns></returns>
        public int ImportByteArrayToFile_CutArray(byte[] bArray, string sFileName)
        {
            Stream sw = File.Create(sFileName);
            sw.Write(bArray, 0, bArray.Length - 1);
            sw.Close();

            return 0;
        }

        public DataTable DataColumnConvSN(DataTable dtInput, string sColumnName)
        {
            DataTable dtOutput = new DataTable();
            DataRow drRow;

            try
            {
                for (int i = 0; i < dtInput.Columns.Count; i++)
                {

                    if (sColumnName.ToUpper() == dtInput.Columns[i].ColumnName.ToUpper())
                    {
                        dtOutput.Columns.Add(dtInput.Columns[i].ColumnName, System.Type.GetType("System.Decimal"));
                    }
                    else
                    {
                        dtOutput.Columns.Add(dtInput.Columns[i].ColumnName);
                    }
                }
                foreach (DataRow dr in dtInput.Rows)
                {
                    drRow = dtOutput.NewRow();
                    for (int j = 0; j < dtInput.Columns.Count; j++)
                    {
                        if (sColumnName.ToUpper() == dtInput.Columns[j].ColumnName.ToUpper())
                        {
                            drRow[j] = decimal.Parse(dr[j].ToString());
                        }
                        else
                        {
                            drRow[j] = dr[j];
                        }
                    }
                    dtOutput.Rows.Add(drRow);
                    //dtOutput.Rows.Add(dr.ItemArray);
                }
            }
            catch (Exception ex)
            {
                AppLogger.Error(ex.Message);
                dtOutput = dtInput;
            }

            return dtOutput;
        }

    }


    /// <summary>
    /// Utility remotizzate.
    /// </summary>
    public class RemoteUtility
    {

        /// <summary>
        /// Prima di seguire questo tutorial, ricordatevi che dovete "sbloccare" il vostro account, consentendo ad app esterne di connettersi.
        /// Senn� il codice non funzioner�(e questo vale per qualsiasi programma scritto in qualsiasi linguaggio).
        /// Dovreste eseguire il login con l'account che volete usare, ed andare a questa pagina: https://myaccount.google.com/u/0/security?hl=it&pli=1#connectedapps
        /// Poi impostare Consenti app meno sicure: ON.
        /// </summary>
        /// <param name="From"></param>
        /// <param name="To"></param>
        /// <param name="Cc"></param>
        /// <param name="Oggetto"></param>
        /// <param name="Message"></param>
        /// <param name="ServerSMTP"></param>
        /// <returns>bool</returns>
        public bool InviaMail(string From, string To, string Cc, string Oggetto, string Message, string ServerSMTP, string User, string Password)
        {
            try
            {
                System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage(From, To, Oggetto, Message);

                //attacco l'indirizzo in cc
                message.CC.Add(new System.Net.Mail.MailAddress(Cc));

                //587 � il numero di porta che mi viene dato con gmail
                System.Net.Mail.SmtpClient mailClient = new System.Net.Mail.SmtpClient(ServerSMTP, 587);

                mailClient.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;

                //le 21 stringe sono nome utente e psw dell'account  che spedisce
                System.Net.NetworkCredential credentials = new System.Net.NetworkCredential(User, Password);
                mailClient.Credentials = credentials;

                mailClient.EnableSsl = true;

                mailClient.Send(message);
            }
            catch (Exception oEx)
            {
                AppLogger.Error("ERRORE nel metodo InviaMail: " + oEx.Message);
                return false;
            }
            return true;

        }
    }

   

    [Serializable]
    public class ErrorCode
    {
        public ErrorCode()
        {
            Code = (int)Codes.OK;
            DescBreve = null;
            DescLunga = null;
            LineNumber = 0;
            HelpLink = null;
        }


        public int Code
        {
            get
            {
                return pCode;
            }
            set
            {
                pCode = value;
            }
        }
        public string DescBreve
        {
            get
            {
                return pDescBreve;
            }
            set
            {
                pDescBreve = value;
            }
        }
        public string DescLunga
        {
            get
            {
                return pDescLunga;
            }
            set
            {
                pDescLunga = value;
            }
        }
        public int LineNumber
        {
            get
            {
                return pLineNumber;
            }
            set
            {
                pLineNumber = value;
            }
        }
        public string HelpLink
        {
            get
            {
                return pHelpLink;
            }
            set
            {
                pHelpLink = value;
            }
        }

        private int pCode;
        private String pDescBreve;
        private String pDescLunga;
        private int pLineNumber;
        private String pHelpLink;
    }

    [Serializable]
    public enum Codes : int
    {

        OK = 1000,
        NON_EXISTING_ANAG,
        PASSWORD_NOK,
        USER_LOCKED_OUT,
        ADDUSER_CHKCODICEUSER_FAILED,
        ADDUSER_CHKNICKNAME_FAILED,
        ADDUSER_NICKNAME_ALREADY_REGISTERED,
        GENERAANAG_ANAGID_ALREADY_REGISTERED,
        GENANAGID_ERROR,
        MAIL_SEND_GENERIC_ERROR,
        NO_CONTATORE,
        NO_CONTRATTO,
        NO_UTENZA,
        NO_UTILITY,
        ERRAUTOLETT02,
        ERRAUTOLETT03,
        ERRAUTOLETT04,
        ERRAUTOLETT05,
        SQL_GENERIC_ERRROR,
        ERRORE_INVIO_COMUNICAZIONE,

        //Errori generici DB
        ERRORE_APERTURA_CONNESSIONE,
        ERRORE_ESECUZIONE_STORED,
        ERRORE_ESECUZIONE_QUERY,
        //

        //caricamenti iniziali
        ERRORE_CARICAMENTO_DATI_UTENTE,
        ERRORE_ASSEGNAMENTO_DATI_UTENTE,
        ERRORE_CARICAMENTO_UTILITY,
        ERRORE_CARICAMENTO_UTENZE,
        ERRORE_CARICAMENTO_CONTRATTI,
        ERRORE_CARICAMENTO_CONTATORI,
        ERRORE_CARICAMENTO_DATI_CONTRATTUALI,
        ERRORE_CARICAMENTO_BOLLETTE,
        ERRORE_CARICAMENTO_RIFIUTI_SPECIALI,
        ERRORE_CARICAMENTO_HELPDESK_UTILITY,
        ERRORE_CARICAMENTO_ALERTS,
        //fine caricamenti iniziali


        //contatore inizio
        ERRORE_DATA_NUOVA_LETTURA_PRECEDENTE_ALLA_DATA_DELL_ULTIMA_LETTURA,
        ERRORE_CONSUMO_MEDIO_ANNUO_NULLO,
        ERRORE_CONSUMO_MEDIO_ANNUO_NON_E_UN_NUMERO,
        ERRORE_CONSUMO_MEDIO_ANNUO_TROPPO_GRANDE_O_PICCOLO,
        ERRORE_LETTURA_DOPPIA_STESSO_GIORNO,
        ERRORE_LETTURA_NULLA,
        ERRORE_LETTURA_NON_E_UN_NUMERO,
        ERRORE_LETTURA_TROPPO_GRANDE_O_PICCOLA,
        ERRORE_DATA_RIFERIMENTO_ULTIMA_LETTURA_NULLA,
        ERRORE_DATA_RIFERIMENTO_ULTIMA_LETTURA_NON_E_UNA_DATA,
        ERRORE_CURVA_TERMICA_NON_IMPOSTATA,
        //contatore fine
        //inizio Pagamenti
        ERRORE_IMPOSSIBILE_CANCELLARE_CC_X_ALLINEAMENTO_IN_CORSO,
        ERRORE_ABICAB_ERRATI,
        ERRORE_CONTOCORRENTE_GIA_IN_ALLINEAMENTO,
        ERRORE_CONTOCORRENTE_GIA_INSERITO,
        ERRORE_INSERIMENTO_ALLINEAMENTO,
        ERRORE_REVOCA_ALLINEAMENTO,
        ERRORE_LISTA_MEZZI_DI_PAGAMENTO,
        ERRORE_INSERIMENTO_MEZZO_DI_PAGAMENTO,
        ERRORE_CANCELLAZIONE_MEZZO_DI_PAGAMENTO,
        ERRORE_DESCRIZIONE_ABICAB,
        ERRORE_LISTA_ALLINEAMENTI,
        ERRORE_INSERISCI_PAGAMENTO,
        ERRORE_LISTA_PAGAMENTI,
        ERRORE_REVOCA_PAGAMENTO,
        ERRORE_DISPOSIZIONE_NON_PRESENTE,
        ERRORE_IMPOSSIBILE_REVOCARE_DISPOSIZIONE_PAGAMENTO,
        ERRORE_CODICE_AUTORIZZAZIONE_ERRATO,
        ERRORE_CODICE_AUTORIZZAZIONE_LOCKEDOUT,
        ERRORE_CODICE_AUTORIZZAZIONE_SCADUTO,
        ERRORE_LINK_CODICE_AUTORIZZAZIONE_NON_IMPOSTATO,
        ERRORE_INVIO_EMAIL_CODICE_AUTORIZZAZIONE,
        ERRORE_GET_URL_CARTA_DI_CREDITO,
        ERRORE_IMPOSTAZIONE_STATO_DISPOSIZIONE_PAGAMENTO,
        ERRORE_CREAZIONE_PAWFILEXML,
        //fine pagamenti
        //generici
        INDIRIZZO_IP_NON_ABILITATO,
        OPERAZIONE_NON_AUTORIZZATA,
        MEZZO_DI_PAGAMENTO_INESISTENTE,
        ERRORE_GENERICO,
        ERRORE_TRASFERIMENTO_FILE,

        ERRORE_USER_HELPDESK
    };


}
