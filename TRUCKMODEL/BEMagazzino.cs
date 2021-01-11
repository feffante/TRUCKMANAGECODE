using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using TRUCKMODEL.Models;
using UtilityBE;

namespace TRUCKMODEL
{
    public class BEMagazzino
    {
        //prelevo dal config il numero 200 dio elementi che posso fare vedere
        int Max = int.Parse(System.Configuration.ConfigurationManager.AppSettings["topItem"].ToString());

        #region ViewMagazzino
        /// <summary>
        /// preleva tutti i valore della vista ViewMagazzino
        /// </summary>
        /// <returns></returns>
        public IQueryable<ViewMagazzino> GetMagazzinoView()
        {
            try
            {
                ViewMagazzino vm = new ViewMagazzino();


                truckmanageContext context = new truckmanageContext();

                var query = from m in context.ViewMagazzino
                            select m;

                return query.Take(Max);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// prelevo tutti i record secondo la key della atbella 
        /// </summary>
        /// <param name="IdMagazzino"></param>
        /// <returns></returns>
        public IQueryable<ViewMagazzino> GetMagazzinoView(int IdMagazzino)
        {
            try
            {
                ViewMagazzino vm = new ViewMagazzino();

                truckmanageContext context = new truckmanageContext();

                var query = from m in context.ViewMagazzino
                            where m.IdMagazzino == IdMagazzino
                            select m;

                return query.Take(Max);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// prelevo tutti i valore della vista ViewMagazzino per il valore di idArticolo
        /// </summary>
        /// <param name="IdArticolo"></param>
        /// <returns></returns>
        public IQueryable<ViewMagazzino> GetMagazzinoView(string codiceArticolo)
        {
            try
            {
                ViewMagazzino vm = new ViewMagazzino();

                truckmanageContext context = new truckmanageContext();

                var query = from m in context.ViewMagazzino
                            where m.CodiceArticolo == codiceArticolo
                            select m;

                return query;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        ///  preleva tutti i valori della vista ViewMagazzino secondo il filtri in input(parametri)
        /// </summary>
        /// <param name="IdArticolo"></param>
        /// <param name="NomeArticolo"></param>
        /// <param name="IdCategoria"></param>
        /// <returns></returns>
        public IQueryable<ViewMagazzino> GetMagazzinoView(string IdArticolo, string NomeArticolo, int IdCategoria)
        {
            try
            {
                ViewMagazzino vm = new ViewMagazzino();


                truckmanageContext context = new truckmanageContext();


                var query =
                    (
                     from m in context.ViewMagazzino
                     where m.CodiceArticolo.StartsWith(IdArticolo)
                    && m.NomeArticolo.StartsWith(NomeArticolo)
                    && (
                            (IdCategoria != 0 && m.IdCategoria == IdCategoria)
                            ||
                            (IdCategoria == 0)
                        )
                     orderby m.CodiceArticolo
                     select m
                     );
                return query.Take(Max);
            }
            catch
            {
                return null;
            }
        }

        public IQueryable<ViewMagazzino> GetMagazzinoView(string IdArticolo, string NomeArticolo, int IdCategoria, int IdMarca, bool flag)
        {
            try
            {
                ViewMagazzino vm = new ViewMagazzino();


                truckmanageContext context = new truckmanageContext();


                var query =
                    (
                     from m in context.ViewMagazzino
                     where m.CodiceArticolo.StartsWith(IdArticolo)
                     && m.IdMarca == IdMarca
                    && m.NomeArticolo.StartsWith(NomeArticolo)
                    && (
                            (IdCategoria != 0 && m.IdCategoria == IdCategoria)
                            ||
                            (IdCategoria == 0)
                        )
                     orderby m.CodiceArticolo
                     select m
                     );
                return query.Take(Max);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// preleva tutti i valori della vista ViewMagazzino secondo il filtri in input(parametri)
        /// </summary>
        /// <param name="IdArticolo"></param>
        /// <param name="NomeArticolo"></param>
        /// <returns></returns>
        public IQueryable<ViewMagazzino> GetMagazzinoView(string CodiceArticolo, string NomeArticolo)
        {
            try
            {
                ViewMagazzino vm = new ViewMagazzino();


                truckmanageContext context = new truckmanageContext();

                var query =
                    (
                     from m in context.ViewMagazzino
                     where m.CodiceArticolo.StartsWith(CodiceArticolo)
                    && m.NomeArticolo.StartsWith(NomeArticolo)
                     orderby m.CodiceArticolo
                     select m
                     );
                return query.Take(Max);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// preleva tutti i valori della vista ViewMagazzino secondo il filtri in input(parametri)
        /// </summary>
        /// <param name="IdArticolo"></param>
        /// <param name="NomeArticolo"></param>
        /// <param name="IdMarca"></param>
        /// <param name="flag"> parametro per differenziare la firma dalla chiamata sulla categortia</param>
        /// <returns></returns>
        public IQueryable<ViewMagazzino> GetMagazzinoView(string IdArticolo, string NomeArticolo, int IdMarca, bool flag)
        {
            try
            {
                ViewMagazzino vm = new ViewMagazzino();


                truckmanageContext context = new truckmanageContext();

                var query =
                    (
                     from m in context.ViewMagazzino
                     where m.CodiceArticolo.StartsWith(IdArticolo)
                    && m.NomeArticolo.StartsWith(NomeArticolo)
                    && m.IdMarca == IdMarca
                     orderby m.CodiceArticolo
                     select m
                     );
                return query.Take(Max);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// preleva tutti i valori della vista ViewMagazzino secondo il filtri in input(parametri)
        /// </summary>
        /// <param name="IdArticolo"></param>
        /// <param name="NomeArticolo"></param>
        /// <param name="CodiceBarre"></param>
        /// <param name="IdCategoria"></param>
        /// <returns></returns>
        public IQueryable<ViewMagazzino> GetMagazzinoView(string IdArticolo, string NomeArticolo, string CodiceBarre, int IdCategoria)
        {
            try
            {
                ViewMagazzino vm = new ViewMagazzino();


                truckmanageContext context = new truckmanageContext();

                var query =
                    (
                     from m in context.ViewMagazzino
                     where m.CodiceArticolo.StartsWith(IdArticolo)
                    && m.NomeArticolo.StartsWith(NomeArticolo)
                    && m.CodiceBarre.StartsWith(CodiceBarre)
                    && (
                            (IdCategoria != 0 && m.IdCategoria == IdCategoria)
                            ||
                            (IdCategoria == 0)
                        )
                     orderby m.CodiceArticolo
                     select m
                     );
                return query.Take(Max);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// preleva tutti gli elementi della vista ViewMagazzino che hanno giacenza minore o = giacenza_min
        /// </summary>
        /// <returns></returns>
        public IQueryable<ViewMagazzino> GetMagazzinoViewOutGiacenza()
        {
            try
            {
                ViewMagazzino vm = new ViewMagazzino();

                truckmanageContext context = new truckmanageContext();

                var query = from m in context.ViewMagazzino
                            where m.Quantita <= m.QuantitaMin
                            select m;

                return query;
            }
            catch
            {
                return null;
            }
        }
        #endregion ViewMagazzino      

        #region MAGAZZINO
        /// <summary>
        /// prelevo l'ultimo id del contatore (key primary) della tabella magazzino
        /// </summary>
        /// <returns></returns>
        public int Get200Mag()
        {
            int iret = -1;
            try
            {
                truckmanageContext context = new truckmanageContext();
                var query = (from m in context.Magazzino
                             select m.IdMagazzino).Max();
                iret = query;
            }
            catch (Exception oEx)
            {
                AppLogger.Error("ERRORE nel metodo Get200Mag: " + oEx.Message);
            }
            return iret;
        }

        /// <summary>
        /// preleva gli elementi della atbella MAGAZZINO secondo la clausola top
        /// </summary>
        /// <returns></returns>
        public IQueryable<Magazzino> GetMag()
        {
            try
            {
                truckmanageContext context = new truckmanageContext();

                var query = from m in context.Magazzino
                            select m;

                return query.Take(Max);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// conta il numero totale degli elementi presenti nella tabella
        /// </summary>
        /// <returns></returns>
        public int GetCountMag()
        {
            int iRet = 1;
            try
            {
                using (truckmanageContext context = new truckmanageContext())
                {
                    var query = (from m in context.Magazzino
                                 select m.IdMagazzino).Count();
                    iRet = query;
                }
            }
            catch (Exception oEx)
            {
                AppLogger.Error("ERRORE nel metodo GetCountMag: " + oEx.Message);
                return -1;
            }
            return iRet;

        }


        /// <summary>
        /// preleva il valore della tabella MAGAZZINO secondo il filtro sulla chiave
        /// </summary>
        /// <param name="IdKey"></param>
        /// <returns></returns>
        public IQueryable<Magazzino> GetMag(int IdKey)
        {
            try
            {
                truckmanageContext context = new truckmanageContext();

                var query = from m in context.Magazzino
                            where m.IdMagazzino == IdKey
                            select m;

                return query;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// preleva il valore della tabella MAGAZZINO secondo il filtro
        /// </summary>
        /// <param name="idCategoria"></param>
        /// <returns></returns>
        public IQueryable<Magazzino> GetMagXCat(int idCategoria)
        {
            try
            {
                truckmanageContext context = new truckmanageContext();

                var query = from m in context.Magazzino
                            where m.IdCategoria == idCategoria
                            select m;

                return query;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// preleva il valore della tabella MAGAZZINO secondo il filtro
        /// </summary>
        /// <param name="codiceArticolo"></param>
        /// <returns></returns>
        public IQueryable<Magazzino> GetMag(string codiceArticolo)
        {
            try
            {
                truckmanageContext context = new truckmanageContext();

                var query = from m in context.Magazzino
                            where m.CodiceArticolo == codiceArticolo
                            select m;

                return query;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// preleva il valore della tabella MAGAZZINO secondo il filtro
        /// </summary>
        /// <param name="CodiceArticolo"></param>
        /// <param name="NomeArticolo"></param>
        /// <returns></returns>
        public IQueryable<Magazzino> GetMag(string codiceArticolo, string nomeArticolo)
        {
            try
            {
                truckmanageContext context = new truckmanageContext();

                var query = from m in context.Magazzino
                            where m.CodiceArticolo == codiceArticolo
                            && m.NomeArticolo == nomeArticolo
                            select m;

                return query;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// preleva tutti i valori della tabella magazzino secondo il filtro in like sul ID_ARTICOLO
        /// </summary>
        /// <param name="IdArticolo"></param>
        /// <returns></returns>
        public IQueryable<string> GetIdArticoloToMagLike(string codiceArticolo)
        {
            try
            {
                truckmanageContext context = new truckmanageContext();

                var query = from m in context.Magazzino
                            where m.CodiceArticolo.StartsWith(codiceArticolo.Trim())
                            select m.CodiceArticolo;

                return query;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// aggiorna un elemento MAGAZZINO esistente
        /// </summary>
        /// <param name="idMag"></param>
        /// <param name="idArticolo"></param>
        /// <param name="idCategoria"></param>
        /// <param name="idFornitore"></param>
        /// <param name="idIva"></param>
        /// <param name="idUnitaMisura"></param>
        /// <param name="NomeArticolo"></param>
        /// <param name="Note"></param>
        /// <param name="PrezzoUni"></param>
        /// <param name="Quantita"></param>
        /// <param name="QuantitaMin"></param>
        /// <param name="FlagScarico"></param>
        /// <param name="CodiceBarre"></param>
        /// <param name="ImagePath"></param>
        /// <returns></returns>
        public int UpdateMag(int idMag, int idMarca, string codiceArticolo, string idArticolo, int idCategoria, int idFornitore, int idIva, int idUnitaMisura, string NomeArticolo
                             , string Note, decimal PrezzoUni, double Quantita, double QuantitaMin, bool FlagScarico, string CodiceBarre, string ImagePath)
        {
            int iRet = 1;
            try
            {
                using (truckmanageContext context = new truckmanageContext())
                {
                    //prelevo un solo elemento in base alla chiave(quello da aggiornare)devo sempre usare il contex!                  
                    var Vm = context.Magazzino.Where(c => c.IdMagazzino == idMag).First();

                    //aggiorno tutti i campi dell'oggetto 
                    Vm.IdMarca = idMarca;
                    Vm.IdArticolo = idArticolo;
                    Vm.CodiceArticolo = codiceArticolo;
                    Vm.IdCategoria = idCategoria;
                    Vm.IdFornitore = idFornitore;
                    Vm.IdIva = idIva;
                    Vm.IdMagazzino = idMag;
                    Vm.IdUnitaMisura = idUnitaMisura;
                    Vm.NomeArticolo = NomeArticolo;
                    Vm.Note = Note;
                    Vm.PrezzoUnitario = PrezzoUni;
                    Vm.Quantita = Quantita;
                    Vm.QuantitaMin = QuantitaMin;
                    Vm.FlagScarico = FlagScarico;
                    Vm.CodiceBarre = CodiceBarre;
                    Vm.ImagePath = ImagePath;

                    //rendo ufficiali le modifiche anche sul db
                    context.SaveChanges();
                }
            }
            catch (Exception oEx)
            {
                AppLogger.Error("ERRORE nel metodo UpdateMag: " + oEx.Message);
                return -1;
            }
            return iRet;
        }


        /// <summary>
        /// aggiorna solo il campo quantita di un elemento magazzino
        /// </summary>
        /// <param name="idMag"></param>
        /// <param name="NewQuantita"></param>
        /// <returns></returns>
        public int UpdateMagToCarico(int idMag, double NewQuantita)
        {
            int iRet = 1;
            try
            {
                using (truckmanageContext context = new truckmanageContext())
                {
                    //prelevo un solo elemento in base alla chiave(quello da aggiornare)devo sempre usare il contex!                  
                    var Vm = context.Magazzino.Where(c => c.IdMagazzino == idMag).First();

                    //aggiorno tutti i campi dell'oggetto 
                    Vm.IdMagazzino = idMag;
                    Vm.Quantita = Vm.Quantita + NewQuantita;

                    //rendo ufficiali le modifiche anche sul db
                    context.SaveChanges();
                }
            }
            catch (Exception oEx)
            {
                AppLogger.Error("ERRORE nel metodo UpdateMagToCarico: " + oEx.Message);
                return -1;
            }
            return iRet;
        }


        /// <summary>
        /// creo un nuovo elemento MAGAZZINO
        /// </summary>
        /// <param name="idArticolo"></param>
        /// <param name="idCategoria"></param>
        /// <param name="idFornitore"></param>
        /// <param name="idIva"></param>
        /// <param name="idUnitaMisura"></param>
        /// <param name="NomeArticolo"></param>
        /// <param name="Note"></param>
        /// <param name="PrezzoUni"></param>
        /// <param name="Quantita"></param>
        /// <param name="QuantitaMin"></param>
        /// <param name="FlagScarico"></param>
        /// <param name="CodiceBarre"></param>
        /// <param name="ImagePath"></param>
        /// <returns></returns>
        public int SaveNewMag(int idMarca, string idArticolo, decimal PrezzoUni, int idMagazzino, string codiceArticolo, int idCategoria, int idFornitore, int idIva,
            int idUnitaMisura, string nomeArticolo, string note, double quantita, double quantitaMin,
            bool flagScarico, string codiceBarre, string imagePath)
        {
            int iRet = -1;
            try
            {
                //prelevo il 200 della key della tabella
                int i200IdMag = this.Get200Mag();

                //creo il nuovo elemento del contex
                using (truckmanageContext context = new truckmanageContext())
                {
                    var NewMagazzino = new Magazzino();

                    NewMagazzino.IdMarca = idMarca;
                    NewMagazzino.IdArticolo = (i200IdMag + 1).ToString();
                    NewMagazzino.IdMagazzino = i200IdMag + 1;
                    NewMagazzino.CodiceBarre = codiceBarre;
                    NewMagazzino.FlagScarico = flagScarico;
                    NewMagazzino.CodiceArticolo = codiceArticolo;
                    NewMagazzino.IdCategoria = idCategoria;
                    NewMagazzino.IdFornitore = idFornitore;
                    NewMagazzino.IdIva = idIva;
                    NewMagazzino.IdUnitaMisura = idUnitaMisura;
                    NewMagazzino.ImagePath = imagePath;
                    NewMagazzino.NomeArticolo = nomeArticolo;
                    NewMagazzino.Note = note;
                    NewMagazzino.PrezzoUnitario = PrezzoUni;
                    NewMagazzino.Quantita = quantita;
                    NewMagazzino.QuantitaMin = quantitaMin;

                    //salvo definitivamente sul db
                    context.Magazzino.Add(NewMagazzino);
                    context.SaveChanges();
                    iRet = 1;
                }

            }
            catch (Exception oEx)
            {
                AppLogger.Error("ERRORE nel metodo SaveNewMag: " + oEx.Message);
                return -1;
            }
            return iRet;
        }

        /// <summary>
        /// cancello l'identita MAGAZZINO per la key. Ritorno int(-1= errore 1=tutto k)
        /// </summary>
        /// <param name="IdKey"></param>
        public int DeleteMagazzino(int IdKey)
        {
            int iRet = -1;
            try
            {
                using (truckmanageContext context = new truckmanageContext())
                {
                    var query = from m in context.Magazzino
                                where m.IdMagazzino == IdKey
                                select m;

                    foreach (Magazzino m in query)
                        context.Magazzino.Remove(m);

                    context.SaveChanges();
                    iRet = 1;
                }
            }
            catch
            {
                iRet = -1;
            }

            return iRet;
        }

        public IQueryable GetMagPrezzoTot(int IdKey)
        {
            try
            {
                truckmanageContext context = new truckmanageContext();
                //let PREZZO_TOTALE = (((m.PREZZO_UNITARIO/ 100) * m.IVA) + m.PREZZO_UNITARIO) * m.QUANTITA
                var query = from m in context.ViewMagazzino
                            where m.IdMagazzino == IdKey
                            let prezzopercento = m.PrezzoUnitario / 100
                            let prezzoxiva = prezzopercento * m.Iva
                            let prezzofine = prezzoxiva + m.PrezzoUnitario
                            let PREZZO_TOTALE = prezzofine * (decimal)m.Quantita
                            select new
                            {
                                m.CodiceArticolo,
                                m.NomeArticolo,
                                m.Quantita,
                                m.PrezzoUnitario,
                                m.Iva,
                                m.CodiceBarre,
                                PREZZO_TOTALE
                            };

                return query;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// preleva tutti gli elementi dalla tabella MAGAZZINO restituendo il campo nome - descrizione
        /// </summary>
        /// <param name="IdArticolo"></param>
        /// <param name="NomeArticolo"></param>
        /// <param name="IdCategoria"></param>
        /// <returns></returns>
        public IQueryable
            GetMagazzinoNomeDescrizione(string IdArticolo, string NomeArticolo, int IdCategoria)
        {
            try
            {
                Magazzino vm = new Magazzino();


                truckmanageContext context = new truckmanageContext();

                var query =
                    (
                     from m in context.Magazzino
                     where m.CodiceArticolo.StartsWith(IdArticolo)
                    && m.NomeArticolo.StartsWith(NomeArticolo)
                    && (
                            (IdCategoria != 0 && m.IdCategoria == IdCategoria)
                            ||
                            (IdCategoria == 0)
                        )
                     let NOME_COMPLETO = m.CodiceArticolo + " - " + m.NomeArticolo
                     orderby m.CodiceArticolo
                     select new
                     {
                         m.IdMagazzino,
                         m.NomeArticolo,
                         m.CodiceArticolo,
                         NOME_COMPLETO
                     }

                     );
                return query.Take(Max);
            }
            catch
            {
                return null;
            }
        }
        #endregion MAGAZZINO

        #region MAGAZZINO_CATEGORIE       
        /// <summary>
        /// prelevo tutti i valore della tabella magazzino_categoria
        /// </summary>
        /// <returns></returns>
        public IQueryable<MagazzinoCategorie> GetMagCategoria()
        {
            try
            {
                truckmanageContext context = new truckmanageContext();

                var query = from m in context.MagazzinoCategorie
                            orderby m.IdCategoria
                            select m;

                return query;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// prelevo un elemento categoria secondo la key in input
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public IQueryable<MagazzinoCategorie> GetMagCategoria(int key)
        {
            try
            {
                truckmanageContext context = new truckmanageContext();

                var query = from m in context.MagazzinoCategorie
                            where m.IdCategoria == key
                            orderby m.Categoria
                            select m;

                return query;
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// prelevo l'ultimo id del contatore (key primary) della tabella MAGAZZINO_CATEGORIA
        /// </summary>
        /// <returns></returns>
        private int Get200MagCategoria()
        {
            int iret = -1;
            try
            {
                truckmanageContext context = new truckmanageContext();
                var query = (from m in context.MagazzinoCategorie
                             select m.IdCategoria).Max();
                iret = query;
            }
            catch (Exception oEx)
            {
                AppLogger.Error("ERRORE nel metodo Get200MagCat: " + oEx.Message);
            }
            return iret;
        }

        /// <summary>
        /// inserisce un nuovo elemento di CATEGORIA
        /// </summary>
        /// <param name="categoria"></param>
        /// <param name="descrizioneCategoria"></param>
        /// <returns></returns>
        public int SaveNewMagCategoria(string categoria, string descrizioneCategoria)
        {
            int iRet = -1;
            try
            {
                //prelevo il 200 della key della tabella
                int i200IdMagCat = this.Get200MagCategoria();

                //creo il nuovo elemento del contex
                using (truckmanageContext context = new truckmanageContext())
                {
                    var NewMagCategoria = new MagazzinoCategorie();

                    NewMagCategoria.IdCategoria = i200IdMagCat + 1;
                    NewMagCategoria.Categoria = categoria;
                    NewMagCategoria.DescrizioneCategoria = "inserito il " + DateTime.Now.ToLongDateString();


                    //salvo definitivamente sul db
                    context.MagazzinoCategorie.Add(NewMagCategoria);
                    context.SaveChanges();
                    iRet = 1;
                }

            }
            catch (Exception oEx)
            {
                AppLogger.Error("ERRORE nel metodo SaveNewMag: " + oEx.Message);
                return -1;
            }
            return iRet;
        }

        /// <summary>
        /// aggiorno un elemento esistente di CATEGORIA
        /// </summary>
        /// <param name="new_categoria"></param>
        /// <param name="old_categoria"></param>
        /// <param name="descrizioneCategoria"></param>
        /// <returns></returns>
        public int UpdateMagCategoria(string new_categoria, string old_categoria, string descrizioneCategoria)
        {
            int iRet = -1;
            try
            {
                //creo il nuovo elemento del contex
                using (truckmanageContext context = new truckmanageContext())
                {
                    //ricerco la vecchia categoria che devo agiornare con il valore nuovo
                    var Vm = context.MagazzinoCategorie.Where(c => c.Categoria == old_categoria).First();

                    Vm.Categoria = new_categoria;
                    Vm.DescrizioneCategoria = descrizioneCategoria;

                    //aggiorno definitivamente sul db                    
                    context.SaveChanges();
                    iRet = 1;
                }

            }
            catch (Exception oEx)
            {
                AppLogger.Error("ERRORE nel metodo UpdateMagCategoria: " + oEx.Message);
                return -1;
            }
            return iRet;
        }
        #endregion MAGAZZINO_CATEGORIE

        #region MagazzinoUnitaMisure
        /// <summary>
        /// prelevo tutti i valo della tabella Magazzino_UnitaMisure secondio il filtro su IdUnitaMisura
        /// </summary>
        /// <param name="IdCategoria"></param>
        /// <returns></returns>
        public IQueryable<MagazzinoUnitaMisure> GetMagUnitaMisura(int IdKey)
        {
            try
            {
                truckmanageContext context = new truckmanageContext();

                var query = from m in context.MagazzinoUnitaMisure
                            where m.IdUnitaMisura == IdKey
                            select m;

                return query;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// prelevo tutti i valore della tabella magazzino_UnitaMisura
        /// </summary>
        /// <returns></returns>
        public IQueryable<MagazzinoUnitaMisure> GetMagUnitaMisura()
        {
            try
            {
                truckmanageContext context = new truckmanageContext();

                var query = from m in context.MagazzinoUnitaMisure
                            select m;

                return query;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// prelevo il 200 valore della tabella UNITA_MISURE
        /// </summary>
        /// <returns></returns>
        private int Get200MagUnitaMisura()
        {
            int iret = -1;
            try
            {
                truckmanageContext context = new truckmanageContext();
                var query = (from m in context.MagazzinoUnitaMisure
                             select m.IdUnitaMisura).Max();
                iret = query;
            }
            catch (Exception oEx)
            {
                AppLogger.Error("ERRORE nel metodo Get200MagUnitaMisura: " + oEx.Message);
            }
            return iret;
        }

        /// <summary>
        /// iserisce un nuovo elemento MAGAZZINO_UNITA_MISURA
        /// </summary>
        /// <param name="iva"></param>
        /// <param name="descrizioneIva"></param>
        /// <returns></returns>
        public int SaveNewMagUnitaMisura(string unitaMisura, string descrizioneUnitaMisura)
        {
            int iRet = -1;
            try
            {
                //prelevo il 200 della key della tabella
                int i200IdMagCat = this.Get200MagUnitaMisura();

                //creo il nuovo elemento del contex
                using (truckmanageContext context = new truckmanageContext())
                {
                    var NewMagCategoria = new MagazzinoUnitaMisure();

                    NewMagCategoria.IdUnitaMisura = i200IdMagCat + 1;
                    NewMagCategoria.UnitaMisura = unitaMisura;
                    NewMagCategoria.DescrizioneUnitaMisura = "inserito il " + DateTime.Now.ToLongDateString();


                    //salvo definitivamente sul db
                    context.MagazzinoUnitaMisure.Add(NewMagCategoria);
                    context.SaveChanges();
                    iRet = 1;
                }

            }
            catch (Exception oEx)
            {
                AppLogger.Error("ERRORE nel metodo SaveNewMagIva: " + oEx.Message);
                return -1;
            }
            return iRet;
        }

        /// <summary>
        /// aggiorno un elemento esistente nella tabella MAGAZZINO_UNITA_MISURA
        /// </summary>
        /// <param name="new_unita"></param>
        /// <param name="old_unita"></param>
        /// <param name="descrizioneunita"></param>
        /// <returns></returns>
        public int UpdateMagUnitaMisura(string new_unita, string old_unita, string descrizioneunita)
        {
            int iRet = -1;
            try
            {
                //creo il nuovo elemento del contex
                using (truckmanageContext context = new truckmanageContext())
                {
                    //ricerco la vecchia categoria che devo agiornare con il valore nuovo
                    var Vm = context.MagazzinoUnitaMisure.Where(c => c.UnitaMisura == old_unita).First();

                    Vm.UnitaMisura = new_unita;
                    Vm.DescrizioneUnitaMisura = descrizioneunita;

                    //aggiorno definitivamente sul db                    
                    context.SaveChanges();
                    iRet = 1;
                }

            }
            catch (Exception oEx)
            {
                AppLogger.Error("ERRORE nel metodo UpdateMagUnitaMisura: " + oEx.Message);
                return -1;
            }
            return iRet;
        }

        #endregion MagazzinoUnitaMisure

        #region MagazzinoIva
        /// <summary>
        /// prelevo tutti i valori dalla tabella MagazzinoIva
        /// </summary>
        /// <returns></returns>
        public IQueryable<MagazzinoIva> GetMagIva()
        {
            try
            {
                truckmanageContext context = new truckmanageContext();

                var query = from m in context.MagazzinoIva
                            select m;

                return query;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// prelevo il 200 valore della tabella iva
        /// </summary>
        /// <returns></returns>
        private int Get200MagIva()
        {
            int iret = -1;
            try
            {
                truckmanageContext context = new truckmanageContext();
                var query = (from m in context.MagazzinoIva
                             select m.IdIva).Max();
                iret = query;
            }
            catch (Exception oEx)
            {
                AppLogger.Error("ERRORE nel metodo Get200MagIva: " + oEx.Message);
            }
            return iret;
        }

        /// <summary>
        /// iserisce un nuovo elemento MagazzinoIva
        /// </summary>
        /// <param name="iva"></param>
        /// <param name="descrizioneIva"></param>
        /// <returns></returns>
        public int SaveNewMagIva(int iva, string descrizioneIva)
        {
            int iRet = -1;
            try
            {
                //prelevo il 200 della key della tabella
                int i200IdMagCat = this.Get200MagIva();

                //creo il nuovo elemento del contex
                using (truckmanageContext context = new truckmanageContext())
                {
                    var NewMagCategoria = new MagazzinoIva();

                    NewMagCategoria.IdIva = i200IdMagCat + 1;
                    NewMagCategoria.Iva = iva;
                    NewMagCategoria.DescrizioneIva = "inserito il " + DateTime.Now.ToLongDateString();


                    //salvo definitivamente sul db
                    context.MagazzinoIva.Add(NewMagCategoria);
                    context.SaveChanges();
                    iRet = 1;
                }

            }
            catch (Exception oEx)
            {
                AppLogger.Error("ERRORE nel metodo SaveNewMagIva: " + oEx.Message);
                return -1;
            }
            return iRet;
        }

        /// <summary>
        /// aggiorna un elemento esistente in MagazzinoIva
        /// </summary>
        /// <param name="new_iva"></param>
        /// <param name="old_iva"></param>
        /// <param name="descrizioneiva"></param>
        /// <returns></returns>
        public int UpdateMagIva(int new_iva, int old_iva, string descrizioneiva)
        {
            int iRet = -1;
            try
            {
                //creo il nuovo elemento del contex
                using (truckmanageContext context = new truckmanageContext())
                {
                    //ricerco la vecchia categoria che devo agiornare con il valore nuovo
                    var Vm = context.MagazzinoIva.Where(c => c.Iva == old_iva).First();

                    Vm.Iva = new_iva;
                    Vm.DescrizioneIva = descrizioneiva;

                    //aggiorno definitivamente sul db                    
                    context.SaveChanges();
                    iRet = 1;
                }

            }
            catch (Exception oEx)
            {
                AppLogger.Error("ERRORE nel metodo UpdateMagIva: " + oEx.Message);
                return -1;
            }
            return iRet;
        }

        #endregion MagazzinoIva

        #region AnagraficaFornitori
        /// <summary>
        /// prelevo l'ultimo id del contatore (key primary) della tabella AnagraficaFornitori
        /// </summary>
        /// <returns></returns>
        private int Get200AnagFornitori()
        {
            int iret = -1;
            try
            {
                truckmanageContext context = new truckmanageContext();
                var query = (from m in context.AnagraficaFornitori
                             select m.IdAnagFornitore).Max();
                iret = query;
            }
            catch (Exception oEx)
            {
                AppLogger.Error("ERRORE nel metodo Get200AnagForn: " + oEx.Message);
                return 0;
            }
            return iret;
        }

        /// <summary>
        /// preleva tutti i valori della tabella ANAGRAFICA FORNITORI secondo il filtro
        /// </summary>
        /// <returns></returns>
        public List<AnagraficaFornitori> GetAnagFornitori()
        {
            try
            {
                truckmanageContext context = new truckmanageContext();

                var query = from m in context.AnagraficaFornitori
                            select m;

                return query.ToList<AnagraficaFornitori>();
            }
            catch
            {
                return null;
            }
        }


        /// <summary>
        /// preleva il valore della tabella ANAGRAFICA FORNITORI secondo il filtro
        /// </summary>
        /// <param name="nome"></param>
        /// <returns></returns>
        public IQueryable<AnagraficaFornitori> GetAnagFornitori(int idfornitore)
        {
            try
            {
                truckmanageContext context = new truckmanageContext();

                var query = from m in context.AnagraficaFornitori
                            where m.IdAnagFornitore == idfornitore
                            select m;

                return query;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// creo un nuovo elemento AHAGRAFICA_FORNITORE
        /// </summary>
        /// <param name="nome"></param>
        /// <param name="indirizzo"></param>
        /// <param name="citta"></param>
        /// <param name="cap"></param>
        /// <param name="tel"></param>
        /// <param name="fax"></param>
        /// <param name="cell"></param>
        /// <param name="email"></param>
        /// <param name="cfisc"></param>
        /// <param name="piva"></param>
        /// <param name="contatto"></param>
        /// <param name="note"></param>
        /// <param name="idfornitore"></param>
        /// <returns></returns>
        public int SaveNewAnagFornitori(string nome, string indirizzo, string citta, string cap, string tel, string fax, string cell, string email,
                                        string cfisc, string piva, string contatto, string note)
        {
            int iRet = -1;
            try
            {
                //prelevo il 200 della key della tabella
                int i200IdAnagFornitori = this.Get200AnagFornitori();

                //creo il nuovo elemento del contex
                using (truckmanageContext context = new truckmanageContext())
                {
                    var NewAnagFornitore = new AnagraficaFornitori();

                    NewAnagFornitore.IdAnagFornitore = i200IdAnagFornitori + 1;
                    NewAnagFornitore.Cap = cap;
                    NewAnagFornitore.Cell = cell;
                    NewAnagFornitore.Cfisc = cfisc;
                    NewAnagFornitore.Citta = citta;
                    NewAnagFornitore.Contatto = contatto;
                    NewAnagFornitore.Email = email;
                    NewAnagFornitore.Nome = nome;
                    NewAnagFornitore.Indirizzo = indirizzo;
                    NewAnagFornitore.Note = note;
                    NewAnagFornitore.NumFax = fax;
                    NewAnagFornitore.NumTel = tel;
                    NewAnagFornitore.Piva = piva;
                    NewAnagFornitore.CodiceFornitore = 0;

                    //salvo definitivamente sul db
                    context.AnagraficaFornitori.Add(NewAnagFornitore);
                    context.SaveChanges();
                    iRet = 1;
                }
            }
            catch (Exception oEx)
            {
                AppLogger.Error("ERRORE nel metodo SaveNewAnagFornitori: " + oEx.Message);
                return -1;
            }
            return iRet;
        }


        /// <summary>
        /// aggiorno un elemento AnagraficaFornitori esistente
        /// </summary>
        /// <param name="nome"></param>
        /// <param name="indirizzo"></param>
        /// <param name="citta"></param>
        /// <param name="cap"></param>
        /// <param name="tel"></param>
        /// <param name="fax"></param>
        /// <param name="cell"></param>
        /// <param name="email"></param>
        /// <param name="cfisc"></param>
        /// <param name="piva"></param>
        /// <param name="contatto"></param>
        /// <param name="note"></param>
        /// <param name="idfornitore"></param>
        /// <param name="idanagraficafornitore"></param>
        /// <returns></returns>
        public int UpdateAnagFornitori(string nome, string indirizzo, string citta, string cap, string tel, string fax, string cell, string email,
                                        string cfisc, string piva, string contatto, string note, int idanagraficafornitore)
        {
            int iRet = 1;
            try
            {
                using (truckmanageContext context = new truckmanageContext())
                {
                    //prelevo un solo elemento in base alla chiave(quello da aggiornare)devo sempre usare il contex!                  
                    var Vm = context.AnagraficaFornitori.Where(c => c.IdAnagFornitore == idanagraficafornitore).First();

                    //aggiorno tutti i campi dell'oggetto 
                    Vm.Cap = cap;
                    Vm.Cell = cell;
                    Vm.Cfisc = cfisc;
                    Vm.Citta = citta;
                    Vm.Contatto = contatto;
                    Vm.Email = email;
                    Vm.IdAnagFornitore = idanagraficafornitore;
                    Vm.Nome = nome;
                    Vm.Indirizzo = indirizzo;
                    Vm.Note = note;
                    Vm.NumFax = fax;
                    Vm.NumTel = tel;
                    Vm.Piva = piva;

                    //rendo ufficiali le modifiche anche sul db
                    context.SaveChanges();
                }
            }
            catch (Exception oEx)
            {
                AppLogger.Error("ERRORE nel metodo UpdateAnagFornitori: " + oEx.Message);
                return -1;
            }
            return iRet;
        }




        #endregion AnagraficaFornitori

        #region LOG SCARICO
        /// <summary>
        /// prelevo l'ultimo id del contatore (key primary) della tabella MAGAZZINO_LOG_CARICO
        /// </summary>
        /// <returns></returns>
        private int Get200LogCarico()
        {
            int iret = -1;
            try
            {
                truckmanageContext context = new truckmanageContext();
                var query = (from m in context.MagazzinoLogCarico
                             select m.IdCarico).Max();
                iret = query;
            }
            catch (Exception oEx)
            {
                AppLogger.Error("ERRORE nel metodo Get200LogCarico: " + oEx.Message);
            }
            return iret;
        }

        /// <summary>
        /// inserisce un nuovo elemento MAGAZZINO_LOG_CARICO
        /// </summary>
        /// <param name="idMagazzino"></param>
        /// <param name="idUser"></param>
        /// <param name="dtScarico"></param>
        /// <param name="note"></param>
        /// <returns></returns>
        public int SaveNewLogCarico(int idMagazzino, int idUser, DateTime dtCarico, string note)
        {
            int iRet = -1;
            try
            {
                //prelevo il 200 della key della tabella
                int i200IdLogCarico = this.Get200LogCarico();

                //creo il nuovo elemento del contex
                using (truckmanageContext context = new truckmanageContext())
                {
                    var NewLogCarico = new MagazzinoLogCarico();

                    NewLogCarico.IdCarico = i200IdLogCarico + 1;
                    NewLogCarico.IdUser = idUser;
                    NewLogCarico.IdMagazzino = idMagazzino;
                    NewLogCarico.DtCarico = dtCarico;
                    NewLogCarico.Nota = note;


                    //salvo definitivamente sul db
                    context.MagazzinoLogCarico.Add(NewLogCarico);
                    context.SaveChanges();
                    iRet = 1;
                }

            }
            catch (Exception oEx)
            {
                AppLogger.Error("ERRORE nel metodo SaveNewLogCarico: " + oEx.Message);
                return -1;
            }
            return iRet;
        }
        #endregion LOG SCARICO

        #region LOG_SCARICO
        /// <summary>
        /// prelevo l'ultimo id del contatore (key primary) della tabella MAGAZZINO_LOG_SCARICO
        /// </summary>
        /// <returns></returns>
        private int Get200LogScarico()
        {
            int iret = 0;
            try
            {
                truckmanageContext context = new truckmanageContext();
                var query = (from m in context.MagazzinoLogScarico
                             select m.IdScarico).Max();
                iret = query;
            }
            catch (Exception oEx)
            {
                AppLogger.Error("ERRORE nel metodo Get200LogScarico: " + oEx.Message);
            }
            return iret;
        }

        /// <summary>
        /// inserisce un nuovo elemento MAGAZZINO_LOG_SCARICO
        /// </summary>
        /// <param name="idMagazzino"></param>
        /// <param name="idUser"></param>
        /// <param name="dtScarico"></param>
        /// <param name="note"></param>
        /// <returns></returns>
        public int SaveNewLogScarico(int idMagazzino, int idUser, DateTime dtScarico, string note)
        {
            int iRet = -1;
            try
            {
                //prelevo il 200 della key della tabella
                int i200IdLogScarico = this.Get200LogScarico();

                //creo il nuovo elemento del contex
                using (truckmanageContext context = new truckmanageContext())
                {
                    var NewLogScarico = new MagazzinoLogScarico();

                    NewLogScarico.IdScarico = i200IdLogScarico + 1;
                    NewLogScarico.IdUser = idUser;
                    NewLogScarico.IdMagazzino = idMagazzino;
                    NewLogScarico.DtScarico = dtScarico;
                    NewLogScarico.Nota = note;


                    //salvo definitivamente sul db
                    context.MagazzinoLogScarico.Add(NewLogScarico);
                    context.SaveChanges();
                    iRet = 1;
                }

            }
            catch (Exception oEx)
            {
                AppLogger.Error("ERRORE nel metodo SaveNewLogScarico: " + oEx.Message);
                return -1;
            }
            return iRet;
        }
        #endregion LOG_SCARICO

        /// <summary>
        /// aggiunge materiale alla tabella lavorazione_materiale, logga sulla tabella logCarico e aggiorna la tabella magazzino
        /// </summary>
        /// <param name="idMagazzino"></param>
        /// <param name="idManutenzione"></param>
        /// <param name="quantita"></param>
        /// <param name="idImpiegato"></param>
        /// <param name="logNota"></param>
        /// <returns></returns>
        public int AddMateriale(int idMagazzino, int idManutenzione, int quantita, int idImpiegato, string logNota)
        {
            //VALORI DI RITORNO: 10 ELEMENTO GIA ESISTENTE NELLA GRIGLIA

            //devo: (TUTTO IN TRANSAZIONE!)
            //inserire un record nella tabellla LAVORAZIONI_MATERIALE
            //aggiornare il record corrispondente nella tabella magazzino(se il flag è true)
            //inserire un record nella tabella MAGAZZINO_LOG_SCARICO(se il flag è true)            

            int iRet = 0;
            BEManutenzione beMan = new BEManutenzione();
            BEMagazzino beMag = new BEMagazzino();
            truckmanageContext context = new truckmanageContext();

            using (var contextTransazione = new truckmanageContext())
            {
                using (var dbContextTransaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        if (!this.EixtItemMagLavor(idMagazzino, idManutenzione))
                            return 10;
                        else//TUTTA LA LOGICA DI INSERIMENTO
                        {
                            //aggiungo a lavorazione magazzino
                            int insertLavMag = beMan.SaveNewManutenzioneMagazzino(idMagazzino, idManutenzione, quantita);

                            //prelevo il singolo elemento di magazzino interessato
                            var queryMagazzino = beMag.GetMag(idMagazzino).First();

                            //verifico se è abilitato il flag di scarico: se true devo aggiornare la quantità del magazzino e 
                            //loggare l'azione di scarico
                            if (queryMagazzino.FlagScarico)
                            {
                                //calcolo il valore della quantità da aggiornare
                                double dQuantitaCalcolata = queryMagazzino.Quantita - double.Parse(quantita.ToString());

                                //aggiorno il valore della quantità per l'elemento magazzino
                                int magaggiorna = beMag.UpdateMag(queryMagazzino.IdMagazzino, queryMagazzino.IdMarca, queryMagazzino.CodiceArticolo, queryMagazzino.IdArticolo, queryMagazzino.IdCategoria,
                                                                  queryMagazzino.IdFornitore, queryMagazzino.IdIva, queryMagazzino.IdUnitaMisura,
                                                                  queryMagazzino.NomeArticolo, queryMagazzino.Note, queryMagazzino.PrezzoUnitario,
                                                                  dQuantitaCalcolata, queryMagazzino.QuantitaMin, queryMagazzino.FlagScarico,
                                                                  queryMagazzino.CodiceBarre, queryMagazzino.ImagePath);

                                //loggo nella relativa tabella l'azione di scarico
                                int loggo = beMag.SaveNewLogScarico(idMagazzino, idImpiegato, DateTime.Now, logNota);
                            }
                        }
                        dbContextTransaction.Commit();
                    }
                    catch (Exception oEx)
                    {
                        dbContextTransaction.Rollback();
                        AppLogger.Error("ERRORE nel metodo AddMateriale: " + oEx.Message);
                    }
                }
            }
            return iRet;
        }

        public int DeleteMateriale(int idMagazzino, int idIntervento, int quantita, int idImpiegato, string logNota)
        {
            //devo: (TUTTO IN TRANSAZIONE!)
            //cancellare un record dalla LAVORAZIONI_MATERIALE
            //aggiornare il record corrispondente nella tabella MAGAZZINO
            //inserire un record nella tabella MAGAZZINO_LOG_CARICO con messaggio che indica un errore nell'inserimento del materiale

            int iRet = 0;
            BEManutenzione beMan = new BEManutenzione();
            BEMagazzino beMag = new BEMagazzino();
            truckmanageContext context = new truckmanageContext();

            using (var contextTransazione = new truckmanageContext())
            {
                using (var dbContextTransaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        //cancello la manutenzione dal magagazzino
                        int delLavMag = beMan.DeleteLavorazioneMateriale(idIntervento, idMagazzino);

                        //prelevo il singolo elemento di magazzino interessato
                        var queryMagazzino = beMag.GetMag(idMagazzino).First();

                        //verifico se è abilitato il flag di scarico: se true devo aggiornare la quantità del magazzino e 
                        //loggare l'azione di ricarico da errore
                        if (queryMagazzino.FlagScarico)
                        {
                            //calcolo il valore della quantità da aggiornare
                            double dQuantitaCalcolata = queryMagazzino.Quantita + double.Parse(quantita.ToString());

                            //aggiorno il valore della quantità per l'elemento magazzino
                            int magaggiorna = beMag.UpdateMag(queryMagazzino.IdMagazzino, queryMagazzino.IdMarca, queryMagazzino.CodiceArticolo, queryMagazzino.IdArticolo, queryMagazzino.IdCategoria,
                                                                      queryMagazzino.IdFornitore, queryMagazzino.IdIva, queryMagazzino.IdUnitaMisura,
                                                                      queryMagazzino.NomeArticolo, queryMagazzino.Note, queryMagazzino.PrezzoUnitario,
                                                                      dQuantitaCalcolata, queryMagazzino.QuantitaMin, queryMagazzino.FlagScarico,
                                                                      queryMagazzino.CodiceBarre, queryMagazzino.ImagePath);

                            //loggo nella relativa tabella l'azione di scarico
                            int loggo = beMag.SaveNewLogCarico(idMagazzino, idImpiegato, DateTime.Now, logNota);
                        }
                        dbContextTransaction.Commit();
                    }
                    catch (Exception oEx)
                    {
                        dbContextTransaction.Rollback();
                        AppLogger.Error("ERRORE nel metodo DeleteMateriale: " + oEx.Message);
                    }
                }
            }
            return iRet;
        }

        /// <summary>
        /// verifica se esiste già un elemento uguale all'interno della tabella LAVORAZIONI MAGAZZINO
        /// </summary>
        /// <param name="idmagazzino"></param>
        /// <param name="idlavorazione"></param>
        /// <returns></returns>
        private bool EixtItemMagLavor(int idmagazzino, int idlavorazione)
        {
            bool bRet = false;
            try
            {
                truckmanageContext context = new truckmanageContext();
                var query = from m in context.ManutenzioneMagazzino
                            where m.IdManutenzione == idlavorazione
                                && m.IdMagazzino == idmagazzino
                            select m;

                if (query.Count() > 0)
                    bRet = false;
                else
                    bRet = true;
            }
            catch (Exception oEx)
            {
                AppLogger.Error("ERRORE nel metodo EixtItemMagLavor: " + oEx.Message);
            }
            return bRet;
        }
    }
}
