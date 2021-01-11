using System;
using System.Configuration;
using System.Linq;
using UtilityBE;
using Microsoft.Extensions.Configuration;
using TRUCKMODEL.Models;

namespace TRUCKMODEL
{
    public class BEManutenzione
    {
        //prelevo dal config il numero max dio elementi che posso fare vedere
        readonly int Max = int.Parse(System.Configuration.ConfigurationManager.AppSettings["topItem"].ToString());

        //stringa di connessione
        readonly string Connession = ConfigurationManager.ConnectionStrings["truckmanageContextCon"].ToString();

        #region MANUTENZIONE

        /// <summary>
        /// prelevo l'ultimo id del contatore (key primary) della tabella Manutenzioni
        /// </summary>
        /// <returns></returns>
        private int GetMaxMan()
        {
            int iret = -1;
            try
            {
                truckmanageContext context = new truckmanageContext();
                var query = (from m in context.Manutenzioni
                             select m.IdManutenzione).Max();
                iret = query;
            }
            catch (Exception oEx)
            {
                UtilityBE.AppLogger.Error("ERRORE nel metodo GetMaxMan: " + oEx.Message);
            }
            return iret;
        }

        /// <summary>
        /// conta il numero totale degli elementi presenti nella tabella
        /// </summary>
        /// <returns></returns>
        public int GetCountMan()
        {
            int iRet = 1;
            try
            {
                using (truckmanageContext context = new truckmanageContext())
                {
                    var query = (from m in context.Manutenzioni
                                 select m.IdManutenzione).Count();
                    iRet = query;
                }
            }
            catch (Exception oEx)
            {
                UtilityBE.AppLogger.Error("ERRORE nel metodo GetCountMan: " + oEx.Message);
                return -1;
            }
            return iRet;

        }

        /// <summary>
        /// preleva gli elementi della atbella MANUTENZIONE secondo la clausola top
        /// </summary>
        /// <returns></returns>
        public IQueryable<Manutenzioni> GetMan()
        {
            try
            {
                truckmanageContext context = new truckmanageContext();

                var query = from m in context.Manutenzioni
                            select m;

                return query.Take(Max);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// preleva il valore della tabella MANUTENZIONE secondo il filtro sulla chiave
        /// </summary>
        /// <param name="IdKey"></param>
        /// <returns></returns>
        public IQueryable<Manutenzioni> GetMan(int IdKey)
        {
            try
            {
                truckmanageContext context = new truckmanageContext();

                var query = from m in context.Manutenzioni
                            where m.IdManutenzione == IdKey
                            select m;

                return query;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// preleva il valore della tabella MANUTENZIONE secondo il filtro
        /// </summary>
        /// <param name="IdMezzo"></param>
        /// <returns></returns>
        public IQueryable<Manutenzioni> GetManXMezzo(int IdMezzo)
        {
            try
            {
                truckmanageContext context = new truckmanageContext();

                var query = from m in context.Manutenzioni
                            where m.IdMezzo == IdMezzo
                            select m;

                return query;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// aggiorno un elemento MANUTENZIONE esistente
        /// </summary>
        /// <param name="idManutenzione"></param>
        /// <param name="dtInizio"></param>
        /// <param name="dtFine"></param>
        /// <param name="Descrizione"></param>
        /// <param name="prezzoTotale"></param>
        /// <param name="unitaMisuraValore"></param>
        /// <param name="Note"></param>
        /// <param name="idManutenzioneTipo"></param>
        /// <param name="IdMezzo"></param>
        /// <param name="idUnitaMusura"></param>
        /// <returns></returns>
        public int UpdateManutenzione(int idManutenzione, DateTime dtInizio, DateTime dtFine, string Descrizione, decimal prezzoTotale,
                              int unitaMisuraValore, string Note, int idManutenzioneTipo, int IdMezzo, int idUnitaMusura)
        {
            int iRet = 1;
            try
            {
                using (truckmanageContext context = new truckmanageContext())
                {

                    //prelevo un solo elemento in base alla chiave(quello da aggiornare)devo sempre usare il contex!                  
                    var Vm = context.Manutenzioni.Where(c => c.IdManutenzione == idManutenzione).First();

                    //aggiorno tutti i campi dell'oggetto 
                    Vm.IdManutenzione = idManutenzione;
                    Vm.IdManutenzioneTipo = idManutenzioneTipo;
                    Vm.IdMezzo = IdMezzo;
                    Vm.IdUnitaMisura = idUnitaMusura;
                    Vm.Note = Note;
                    Vm.UmValore = unitaMisuraValore;
                    Vm.PrezzoTot = prezzoTotale;
                    Vm.Descrizione = Descrizione;
                    Vm.DtFine = dtFine;
                    Vm.DtInizio = dtInizio;

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
        /// inserisce un nuovo elemento nella cartella MANUTENZIONE
        /// </summary>
        /// <param name="idManutenzione"></param>
        /// <param name="dtInizio"></param>
        /// <param name="dtFine"></param>
        /// <param name="Descrizione"></param>
        /// <param name="prezzoTotale"></param>
        /// <param name="unitaMisuraValore"></param>
        /// <param name="Note"></param>
        /// <param name="idManutenzioneTipo"></param>
        /// <param name="IdMezzo"></param>
        /// <param name="idUnitaMusura"></param>
        /// <returns></returns>
        public int SaveNewMnutenzione(int idManutenzione, DateTime dtInizio, DateTime dtFine, string Descrizione, decimal prezzoTotale,
                              int unitaMisuraValore, string Note, int idManutenzioneTipo, int IdMezzo, int idUnitaMusura)
        {
            int iRet = -1;
            try
            {
                //prelevo il max della key della tabella
                int iMaxIdMag = this.GetMaxMan();

                //creo il nuovo elemento del contex
                using (truckmanageContext context = new truckmanageContext())
                {
                    var NewManutenzione = new Manutenzioni
                    {
                        IdManutenzione = iMaxIdMag + 1,
                        IdManutenzioneTipo = idManutenzioneTipo,
                        IdMezzo = IdMezzo,
                        IdUnitaMisura = idUnitaMusura,
                        Note = Note,
                        PrezzoTot = prezzoTotale,
                        UmValore = unitaMisuraValore,
                        Descrizione = Descrizione,
                        DtFine = dtFine,
                        DtInizio = dtInizio
                    };

                    //salvo definitivamente sul db
                    context.Manutenzioni.Add(NewManutenzione);
                    context.SaveChanges();
                    //iRet = 1;
                    iRet = NewManutenzione.IdManutenzione;
                }
            }
            catch (Exception oEx)
            {
                AppLogger.Error("ERRORE nel metodo SaveNewMan: " + oEx.Message);
                return -1;
            }
            return iRet;
        }

        /// <summary>
        /// cancello l'identita MANUTENZIONE per la key. Ritorno int(-1= errore 1=tutto k)
        /// </summary>
        /// <param name="IdKey"></param>
        public int DeleteManutenzione(int IdKey)
        {
            int iRet = -1;
            try
            {
                using (truckmanageContext context = new truckmanageContext())
                {
                    var query = from m in context.Manutenzioni
                                where m.IdManutenzione == IdKey
                                select m;

                    foreach (Manutenzioni m in query)
                        context.Manutenzioni.Remove(m);

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

        #endregion MANUTENZIONE

        #region MANUTENZIONE_PROGRAMMATE
        /// <summary>
        /// prelevo l'ultimo id del contatore (key primary) della tabella ManutenzioneProgrammata
        /// </summary>
        /// <returns></returns>
        private int GetMaxManProg()
        {
            int iret = -1;
            try
            {
                truckmanageContext context = new truckmanageContext();
                var query = (from m in context.ManutenzioneProgrammata
                             select m.IdManutenzioneProgrammata).Max();
                iret = query;
            }
            catch (Exception oEx)
            {
                UtilityBE.AppLogger.Error("ERRORE nel metodo GetMaxManProg: " + oEx.Message);
            }
            return iret;
        }

        /// <summary>
        /// preleva gli elementi della atbella MANUTENZIONEPROGRAMMATA secondo la clausola top
        /// </summary>
        /// <returns></returns>
        public IQueryable<ManutenzioneProgrammata> GetManProg()
        {
            try
            {
                truckmanageContext context = new truckmanageContext();

                var query = from m in context.ManutenzioneProgrammata
                            select m;

                return query.Take(Max);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// preleva dalla tabella ManutenzioneProgrammata tutti i record che hanno il campo ISCLOSE = true
        /// </summary>
        /// <returns></returns>
        public IQueryable<ManutenzioneProgrammata> GetManProgClosed()
        {
            try
            {
                truckmanageContext context = new truckmanageContext();

                var query = from m in context.ManutenzioneProgrammata
                            where m.ManutenzioneChiusa == true
                            select m;

                return query.Take(Max);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// preleva il valore della tabella ManutenzioneProgrammata secondo il filtro sulla chiave
        /// </summary>
        /// <param name="IdKey"></param>
        /// <returns></returns>
        public IQueryable<ManutenzioneProgrammata> GetManProg(int IdKey)
        {
            try
            {
                truckmanageContext context = new truckmanageContext();

                var query = from m in context.ManutenzioneProgrammata
                            where m.IdManutenzioneProgrammata == IdKey
                            select m;

                return query;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// inserisce un nuovo elemento nella cartella ManutenzioneProgrammata
        /// </summary>
        /// <param name="idTipoManutenzioneProg"></param>
        /// <param name="IdMezzo"></param>
        /// <param name="kmGiornalieri"></param>
        /// <param name="kmDeltaProssimoIntervento"></param>
        /// <param name="kmPresunti"></param>
        /// <param name="kmReali"></param>
        /// <param name="dtInserimento"></param>
        /// <param name="dtPrevista"></param>
        /// <param name="giorniPreavviso"></param>
        /// <param name="isClose"></param>
        /// <returns>ID inserito</returns>
        public int SaveNewManutenzioneProg(int idTipoManutenzioneProg, int IdMezzo, long kmGiornalieri, long kmDeltaProssimoIntervento,
            long kmPresunti, long kmReali, DateTime dtInserimento, DateTime dtPrevista, int giorniPreavviso, bool isClose)
        {
            int iRet = -1;
            try
            {
                //prelevo il max della key della tabella
                int iMaxIdManutenzioneProg = this.GetMaxManProg();

                //creo il nuovo elemento del contex
                using (truckmanageContext context = new truckmanageContext())
                {
                    var NewManutenzioneProg = new ManutenzioneProgrammata
                    {
                        IdManutenzioneProgrammata = iMaxIdManutenzioneProg + 1,
                        IdTipoManutenzioneProgrammata = idTipoManutenzioneProg,
                        IdMezzo = IdMezzo,
                        DtInserimento = dtInserimento,
                        DtPrevista = dtPrevista,
                        KmDeltaProssimoIntervento = kmDeltaProssimoIntervento,
                        KmGiornalieri = kmGiornalieri,
                        KmPresunti = kmPresunti,
                        KmReali = kmReali,
                        GiorniPreavviso = giorniPreavviso,
                        ManutenzioneChiusa = isClose

                    };

                    //salvo definitivamente sul db
                    context.ManutenzioneProgrammata.Add(NewManutenzioneProg);
                    context.SaveChanges();
                    //iRet = 1;
                    iRet = NewManutenzioneProg.IdManutenzioneProgrammata;
                }
            }
            catch (Exception oEx)
            {
                AppLogger.Error("ERRORE nel metodo SaveNewManutenzioneProg: " + oEx.Message);
                return -1;
            }
            return iRet;
        }

        /// <summary>
        ///  aggiorno un elemento ManutenzioneProgrammata esistente
        /// </summary>
        /// <param name="idManutenzioneProgrammata"></param>
        /// <param name="idTipoManutenzioneProg"></param>
        /// <param name="IdMezzo"></param>
        /// <param name="kmGiornalieri"></param>
        /// <param name="kmDeltaProssimoIntervento"></param>
        /// <param name="kmPresunti"></param>
        /// <param name="kmReali"></param>
        /// <param name="dtInserimento"></param>
        /// <param name="dtPrevista"></param>
        /// <param name="giorniPreavviso"></param>
        /// <param name="isClose"></param>
        /// <returns></returns>
        public int UpdateManutenzioneProg(int idManutenzioneProgrammata, int idTipoManutenzioneProg, int IdMezzo, long kmGiornalieri, long kmDeltaProssimoIntervento,
            long kmPresunti, long kmReali, DateTime dtInserimento, DateTime dtPrevista, int giorniPreavviso, bool isClose)
        {
            int iRet = 1;
            try
            {
                using (truckmanageContext context = new truckmanageContext())
                {

                    //prelevo un solo elemento in base alla chiave(quello da aggiornare)devo sempre usare il contex!                  
                    var Vm = context.ManutenzioneProgrammata.Where(c => c.IdManutenzioneProgrammata == idManutenzioneProgrammata).First();

                    //aggiorno tutti i campi dell'oggetto 
                    Vm.IdManutenzioneProgrammata = idManutenzioneProgrammata;
                    Vm.IdTipoManutenzioneProgrammata = idTipoManutenzioneProg;
                    Vm.IdMezzo = IdMezzo;
                    Vm.DtInserimento = dtInserimento;
                    Vm.DtPrevista = dtPrevista;
                    Vm.GiorniPreavviso = giorniPreavviso;
                    Vm.KmDeltaProssimoIntervento = kmDeltaProssimoIntervento;
                    Vm.KmGiornalieri = kmGiornalieri;
                    Vm.KmPresunti = kmPresunti;
                    Vm.KmReali = kmReali;
                    Vm.ManutenzioneChiusa = isClose;

                    //rendo ufficiali le modifiche anche sul db
                    context.SaveChanges();
                }
            }
            catch (Exception oEx)
            {
                AppLogger.Error("ERRORE nel metodo UpdateManutenzioneProg: " + oEx.Message);
                return -1;
            }
            return iRet;
        }

        /// <summary>
        /// aggiorna solo il campo MANUTENZIONE_CHIUSA 
        /// </summary>
        /// <param name="idManutenzioneProgrammata"></param>
        /// <returns></returns>
        public int UpdateManutenzioneProgToClose(int idManutenzioneProgrammata)
        {
            int iRet = 1;
            try
            {
                using (truckmanageContext context = new truckmanageContext())
                {

                    //prelevo un solo elemento in base alla chiave(quello da aggiornare)devo sempre usare il contex!                  
                    var Vm = context.ManutenzioneProgrammata.Where(c => c.IdManutenzioneProgrammata == idManutenzioneProgrammata).First();

                    //aggiorno tutti i campi dell'oggetto 
                    Vm.IdManutenzioneProgrammata = idManutenzioneProgrammata;
                    Vm.ManutenzioneChiusa = true;

                    //rendo ufficiali le modifiche anche sul db
                    context.SaveChanges();
                }
            }
            catch (Exception oEx)
            {
                AppLogger.Error("ERRORE nel metodo UpdateManutenzioneProgToClose: " + oEx.Message);
                return -1;
            }
            return iRet;
        }


        /// <summary>
        /// cancella un elemento della entità ManutenzioneProgrammata secondo la key di filtro
        /// </summary>
        /// <param name="IdKey"></param>
        /// <returns></returns>
        public int DeleteManutenzioneProg(int IdKey)
        {
            int iRet = -1;
            try
            {
                using (truckmanageContext context = new truckmanageContext())
                {
                    var query = from m in context.ManutenzioneProgrammata
                                where m.IdManutenzioneProgrammata == IdKey
                                select m;

                    foreach (ManutenzioneProgrammata m in query)
                        context.ManutenzioneProgrammata.Remove(m);

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

        #endregion MANUTENZIONE_PROGRAMMATE

        #region ManutenzioneProgrammata_STORICO

        private int GetMaxManProgStorico()
        {
            int iret = -1;
            try
            {
                truckmanageContext context = new truckmanageContext();
                var query = (from m in context.ViewManutenzioneProgrammataStorico
                             select m.IdManutenzioneProgrammata).Max();
                iret = query;
            }
            catch (Exception oEx)
            {
                UtilityBE.AppLogger.Error("ERRORE nel metodo GetMaxManProgStorico: " + oEx.Message);
            }
            return iret;
        }

        /// <summary>
        /// preleva gli elementi della atbella MANUTENZIONEPROGRAMMATASTORICO secondo la clausola top
        /// </summary>
        /// <returns></returns>
        public IQueryable<ManutenzioneProgrammataStorico> GetManProgStorico()
        {
            try
            {
                truckmanageContext context = new truckmanageContext();

                var query = from m in context.ManutenzioneProgrammataStorico
                            where m.ManutenzioneChiusa == true
                            select m;

                return query.Take(Max);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// preleva il valore della tabella ManutenzioneProgrammata_STORICO secondo il filtro sulla chiave
        /// </summary>
        /// <param name="IdKey"></param>
        /// <returns></returns>
        public IQueryable<ManutenzioneProgrammataStorico> GetManProgStorico(int IdKey)
        {
            try
            {
                truckmanageContext context = new truckmanageContext();

                var query = from m in context.ManutenzioneProgrammataStorico
                            where m.IdManutenzioneProgrammata == IdKey
                            select m;

                return query;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// inserisce un nuovo elemento nella cartella ManutenzioneProgrammata
        /// </summary>
        /// <param name="idTipoManutenzioneProg"></param>
        /// <param name="IdMezzo"></param>
        /// <param name="kmGiornalieri"></param>
        /// <param name="kmDeltaProssimoIntervento"></param>
        /// <param name="kmPresunti"></param>
        /// <param name="kmReali"></param>
        /// <param name="dtInserimento"></param>
        /// <param name="dtPrevista"></param>
        /// <param name="giorniPreavviso"></param>
        /// <param name="isClose"></param>
        /// <returns>ID inserito</returns>
        public int SaveNewManutenzioneProgStorico(int idTipoManutenzioneProg, int IdMezzo, long kmGiornalieri, long kmDeltaProssimoIntervento,
            long kmPresunti, long kmReali, DateTime dtInserimento, DateTime dtPrevista, int giorniPreavviso, bool isClose)
        {
            int iRet = -1;
            try
            {
                //prelevo il max della key della tabella
                int iMaxIdManutenzioneProgStorico = this.GetMaxManProgStorico();

                //creo il nuovo elemento del contex
                using (truckmanageContext context = new truckmanageContext())
                {
                    var NewManutenzioneProgStorico = new ManutenzioneProgrammataStorico
                    {
                        IdManutenzioneProgrammata = iMaxIdManutenzioneProgStorico + 1,
                        IdTipoManutenzioneProgrammata = idTipoManutenzioneProg,
                        IdMezzo = IdMezzo,
                        DtInserimento = dtInserimento,
                        DtPrevista = dtPrevista,
                        KmDeltaProssimoIntervento = kmDeltaProssimoIntervento,
                        KmGiornalieri = kmGiornalieri,
                        KmPresunti = kmPresunti,
                        KmReali = kmReali,
                        GiorniPreavviso = giorniPreavviso,
                        ManutenzioneChiusa = isClose

                    };

                    //salvo definitivamente sul db
                    context.ManutenzioneProgrammataStorico.Add(NewManutenzioneProgStorico);
                    context.SaveChanges();
                    //iRet = 1;
                    iRet = NewManutenzioneProgStorico.IdManutenzioneProgrammata;
                }
            }
            catch (Exception oEx)
            {
                AppLogger.Error("ERRORE nel metodo SaveNewManutenzioneProgStorico: " + oEx.Message);
                return -1;
            }
            return iRet;
        }

        #endregion ManutenzioneProgrammata_STORICO

        #region MANUTENZIONE_PROGRAMMATE_TIPO
        /// <summary>
        /// preleva il max del valore ID
        /// </summary>
        /// <returns></returns>
        private int GetMaxManProgTipo()
        {
            int iret = -1;
            try
            {
                truckmanageContext context = new truckmanageContext();
                var query = (from m in context.ManutenzioneProgrammataTipo
                             select m.IdTipoManutenzioneProgrammata).Max();
                iret = query;
            }
            catch (Exception oEx)
            {
                UtilityBE.AppLogger.Error("ERRORE nel metodo GetMaxManProgTipo: " + oEx.Message);
            }
            return iret;
        }

        /// <summary>
        /// preleva tutti gli elementi dell entita ManutenzioneProgrammataTipo
        /// </summary>
        /// <returns></returns>
        public IQueryable<ManutenzioneProgrammataTipo> GetManutenzioneProgTipo()
        {
            try
            {
                truckmanageContext context = new truckmanageContext();

                var query = from m in context.ManutenzioneProgrammataTipo
                            select m;

                return query.Take(Max);
            }
            catch
            {
                return null;
            }
        }


        /// <summary>
        /// preleva tutti gli elementi dell entita ManutenzioneProgrammataTipo secondo il filtro sulla chiave
        /// </summary>
        /// <param name="idTipoManutenzioneProg"></param>
        /// <returns></returns>
        public IQueryable<ManutenzioneProgrammataTipo> GetManutenzioneProgTipo(int idTipoManutenzioneProg)
        {
            try
            {
                truckmanageContext context = new truckmanageContext();

                var query = from m in context.ManutenzioneProgrammataTipo
                            where m.IdTipoManutenzioneProgrammata == idTipoManutenzioneProg
                            select m;

                return query.Take(Max);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// inserisce nella tabella MANUTENZIONE_PROGRAMMATE_TIPO un nnuovo elemento
        /// </summary>
        /// <param name="newtipomanprog"></param>
        /// <returns></returns>
        public int SaveNewManutenzioneTipoProgrammata(string newtipomanprog)
        {
            int iRet = -1;
            try
            {
                int id = this.GetMaxManProgTipo();

                //creo il nuovo elemento del contex
                using (truckmanageContext context = new truckmanageContext())
                {
                    var NewTipoMezzo = new ManutenzioneProgrammataTipo
                    {
                        IdTipoManutenzioneProgrammata = id + 1,
                        TipoManutenzioneProgrammata = newtipomanprog
                    };

                    //salvo definitivamente sul db
                    context.ManutenzioneProgrammataTipo.Add(NewTipoMezzo);
                    context.SaveChanges();
                    iRet = 1;
                }
            }
            catch (Exception oEx)
            {
                AppLogger.Error("ERRORE nel metodo SaveNewManutenzioneTipoProgrammata: " + oEx.Message);
                return -1;
            }
            return iRet;
        }
        #endregion MANUTENZIONE_PROGRAMMATE_TIPO

        #region ViewManutenzioni_PROGRAMMATE_STORICO

        public IQueryable<ViewManutenzioneProgrammataStorico> GetViewManStorico()
        {
            try
            {
                truckmanageContext context = new truckmanageContext();

                var query = from m in context.ViewManutenzioneProgrammataStorico
                            select m;

                return query.Take(Max);
            }
            catch
            {
                return null;
            }
        }


        #endregion ViewManutenzioni_PROGRAMMATE_STORICO

        #region ViewManutenzioni
        /// <summary>
        /// preleva tutti i valori dalla vista VIEW_MANUTENZIONE
        /// </summary>
        /// <returns></returns>
        public IQueryable<ViewManutenzioni> GetViewMan()
        {
            try
            {
                truckmanageContext context = new truckmanageContext();

                var query = from m in context.ViewManutenzioni
                            select m;

                return query.Take(Max);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// preleva tutti i valori dalla vista VIEW_MANUTENZIONE secondo la key IdManutenzione
        /// </summary>
        /// <param name="IdKey"></param>
        /// <returns></returns>
        public IQueryable<ViewManutenzioni> GetViewMan(int IdKey)
        {
            try
            {
                truckmanageContext context = new truckmanageContext();

                var query = from m in context.ViewManutenzioni
                            where m.IdManutenzione == IdKey
                            select m;

                return query;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// preleva tutti i record della vista ViewManutenzioni nel range di date <> data inizio e data fine
        /// </summary>
        /// <param name="dtInizio"></param>
        /// <param name="dtFine"></param>
        /// <returns></returns>
        public IQueryable<ViewManutenzioni> GetViewManFromDate(DateTime dtInizio, DateTime dtFine)
        {
            try
            {
                truckmanageContext context = new truckmanageContext();

                var query = from m in context.ViewManutenzioni
                            where m.DtFine < dtFine &&
                                  m.DtInizio> dtInizio
                            select m;

                return query;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// prelevo tutti gli elementi della vista VIEW_MANUTENZIONE secondo il filtro in like in input
        /// </summary>
        /// <param name="dtInizio"></param>
        /// <param name="dtFine"></param>
        /// <param name="IdMezzo"></param>
        /// <param name="Note"></param>
        /// <param name="Descrizione"></param>
        /// <returns></returns>
        public IQueryable<ViewManutenzioni> GetViewMan(DateTime dtInizio, DateTime dtFine, int idTipoManutenzione, int Idmezzo, string Note, string Descrizione, int idTipoMezzo)
        {
            try
            {
                truckmanageContext context = new truckmanageContext();

               // context.Database.Log = Console.Write;

                var query = from m in context.ViewManutenzioni
                            where (m.DtFine <= dtFine &&
                                  m.DtInizio >= dtInizio) &&
                                  m.IdManutenzioneTipo == idTipoManutenzione &&
                                  m.IdMezzo == Idmezzo &&
                                  m.IdTipoMezzo == idTipoMezzo &&
                                  m.Descrizione.StartsWith(Descrizione) &&
                                  m.Note.StartsWith(Note)
                            select m;

                return query;
            }
            catch
            {
                return null;
            }
        }

        public IQueryable<ViewManutenzioni> GetViewManNoTipoMezzo(DateTime dtInizio, DateTime dtFine, int idTipoManutenzione, int Idmezzo, string Note, string Descrizione)
        {
            try
            {
                truckmanageContext context = new truckmanageContext();

               // context.Database.Log = Console.Write;

                var query = from m in context.ViewManutenzioni
                            where (m.DtFine <= dtFine &&
                                  m.DtInizio >= dtInizio) &&
                                  m.IdManutenzioneTipo == idTipoManutenzione &&
                                  m.IdMezzo == Idmezzo &&
                                  m.Descrizione.StartsWith(Descrizione) &&
                                  m.Note.StartsWith(Note)
                            select m;

                return query;
            }
            catch
            {
                return null;
            }
        }

        public IQueryable<ViewManutenzioni> GetViewMan(int idTipoManutenzione, int Idmezzo, string Note, string Descrizione, int idTipoMezzo)
        {
            try
            {
                truckmanageContext context = new truckmanageContext();

               // context.Database.Log = Console.Write;

                var query = from m in context.ViewManutenzioni
                            where m.IdManutenzioneTipo == idTipoManutenzione &&
                                  m.IdMezzo == Idmezzo &&
                                  m.IdTipoMezzo == idTipoMezzo &&
                                  m.Descrizione.StartsWith(Descrizione) &&
                                  m.Note.StartsWith(Note)
                            select m;

                return query;
            }
            catch
            {
                return null;
            }
        }

        public IQueryable<ViewManutenzioni> GetViewManNoTipoMezzo(int idTipoManutenzione, int Idmezzo, string Note, string Descrizione)
        {
            try
            {
                truckmanageContext context = new truckmanageContext();

               // context.Database.Log = Console.Write;

                var query = from m in context.ViewManutenzioni
                            where m.IdManutenzioneTipo == idTipoManutenzione &&
                                  m.IdMezzo == Idmezzo &&
                                  m.Descrizione.StartsWith(Descrizione) &&
                                  m.Note.StartsWith(Note)
                            select m;

                return query;
            }
            catch
            {
                return null;
            }
        }

        public IQueryable<ViewManutenzioni> GetViewMan(DateTime dtInizio, DateTime dtFine, int idTipoManutenzione, int IdMezzo, string Note, string Descrizione)
        {
            try
            {
                truckmanageContext context = new truckmanageContext();

                //context.Database.Log = Console.Write;

                var query = from m in context.ViewManutenzioni
                            where (m.DtFine <= dtFine &&
                                  m.DtInizio >= dtInizio) &&
                                  m.IdManutenzioneTipo == idTipoManutenzione &&
                                  m.IdMezzo == IdMezzo &&
                                  m.Descrizione.StartsWith(Descrizione) &&
                                  m.Note.StartsWith(Note)
                            select m;

                return query;
            }
            catch
            {
                return null;
            }
        }

        public IQueryable<ViewManutenzioni> GetViewMan(int idTipoManutenzione, int IdMezzo, string Note, string Descrizione)
        {
            try
            {
                truckmanageContext context = new truckmanageContext();

               // context.Database.Log = Console.Write;

                var query = from m in context.ViewManutenzioni
                            where m.IdManutenzioneTipo == idTipoManutenzione &&
                                  m.IdMezzo == IdMezzo &&
                                  m.Descrizione.StartsWith(Descrizione) &&
                                  m.Note.StartsWith(Note)
                            select m;

                return query;
            }
            catch
            {
                return null;
            }
        }

        public IQueryable<ViewManutenzioni> GetViewMan(DateTime dtInizio, DateTime dtFine, string Note, string Descrizione, int idtipomezzo)
        {
            try
            {
                truckmanageContext context = new truckmanageContext();

                var query = from m in context.ViewManutenzioni
                            where (m.DtFine <= dtFine &&
                                  m.DtInizio >= dtInizio) &&
                                  m.Note.StartsWith(Note) &&
                                  m.Descrizione.StartsWith(Descrizione) &&
                                  m.IdTipoMezzo == idtipomezzo
                            select m;

                return query;
            }
            catch
            {
                return null;
            }
        }

        public IQueryable<ViewManutenzioni> GetViewManNoTipoMezzo(DateTime dtInizio, DateTime dtFine, string Note, string Descrizione)
        {
            try
            {
                truckmanageContext context = new truckmanageContext();

                var query = from m in context.ViewManutenzioni
                            where (m.DtFine <= dtFine &&
                                  m.DtInizio >= dtInizio) &&
                                  m.Note.StartsWith(Note) &&
                                  m.Descrizione.StartsWith(Descrizione)
                            select m;

                return query;
            }
            catch
            {
                return null;
            }
        }

        public IQueryable<ViewManutenzioni> GetViewMan(string Note, string Descrizione, int idtipomezzo)
        {
            try
            {
                truckmanageContext context = new truckmanageContext();

                var query = from m in context.ViewManutenzioni
                            where m.Note.StartsWith(Note) &&
                                  m.Descrizione.StartsWith(Descrizione) &&
                                  m.IdTipoMezzo == idtipomezzo
                            select m;

                return query;
            }
            catch
            {
                return null;
            }
        }

        public IQueryable<ViewManutenzioni> GetViewManNoTipoMezzo(string Note, string Descrizione)
        {
            try
            {
                truckmanageContext context = new truckmanageContext();

                var query = from m in context.ViewManutenzioni
                            where m.Note.StartsWith(Note) &&
                                  m.Descrizione.StartsWith(Descrizione)

                            select m;

                return query;
            }
            catch
            {
                return null;
            }
        }

        public IQueryable<ViewManutenzioni> GetViewMan(DateTime dtInizio, DateTime dtFine, string Note, string Descrizione)
        {
            try
            {
                truckmanageContext context = new truckmanageContext();

                var query = from m in context.ViewManutenzioni
                            where (m.DtFine <= dtFine &&
                                  m.DtInizio >= dtInizio) &&
                                  m.Note.StartsWith(Note) &&
                                  m.Descrizione.StartsWith(Descrizione)
                            select m;

                return query;
            }
            catch
            {
                return null;
            }
        }

        public IQueryable<ViewManutenzioni> GetViewMan(string Note, string Descrizione)
        {
            try
            {
                truckmanageContext context = new truckmanageContext();

                var query = from m in context.ViewManutenzioni
                            where m.Note.StartsWith(Note) &&
                                  m.Descrizione.StartsWith(Descrizione)
                            select m;

                return query;
            }
            catch
            {
                return null;
            }
        }
        #endregion ViewManutenzioni

        #region VIEW_ManutenzioneProgrammata
        //  SELECT TOP 1000 [IdManutenzioneProgrammata]
        //,[IdMezzo]
        //,[MARCA]
        //,[Modello]
        //,[TIPO_ManutenzioneProgrammata]
        //,[IdTipoManutenzioneProgrammata]
        //,[KM_GIORNALIERI]
        //,[KM_DELTA_PROSSIMO_INTERVENTO]
        //,[KM_PRESUNTI]
        //,[KM_REALI]
        //,[DT_INSERIMENTO]
        //,[DT_PREVISTA]
        //,[GIORNI_PREAVVISO]
        //,[MANUTENZIONE_CHIUSA]
        //,[Targha]
        //,[DATA_IMM]
        //,[ALLESTIMENTO]
        //  FROM[TRUCKMANAGE].[dbo].[VIEW_ManutenzioneProgrammata]

        /// <summary>
        /// preleva tutti gli elementi dalla vista VIEW_ManutenzioneProgrammata
        /// </summary>
        /// <returns></returns>
        public IQueryable<ViewManutenzioneProgrammata> GetViewManProg()
        {
            try
            {
                truckmanageContext context = new truckmanageContext();

                var query = from m in context.ViewManutenzioneProgrammata
                            select m;

                return query.Take(Max);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// preleva tutti gli elementi dalla vista VIEW_ManutenzioneProgrammata secondo il filtro sulla key
        /// </summary>
        /// <param name="IdKey"></param>
        /// <returns></returns>
        public IQueryable<ViewManutenzioneProgrammata> GetViewManProg(int IdKey)
        {
            try
            {
                truckmanageContext context = new truckmanageContext();

                var query = from m in context.ViewManutenzioneProgrammata
                            where m.IdManutenzioneProgrammata == IdKey
                            select m;

                return query;
            }
            catch
            {
                return null;
            }
        }


        #endregion VIEW_ManutenzioneProgrammata

        #region VIEW_MEZZI
        /// <summary>
        /// preleva tutti gli elementi della vista VIEW_MEZZI
        /// </summary>
        /// <returns></returns>
        public IQueryable<ViewMezzi> GetViewMezzi()
        {
            try
            {
                truckmanageContext context = new truckmanageContext();

                var query = from m in context.ViewMezzi
                            select m;

                return query.Take(Max);
            }
            catch
            {
                return null;
            }
        }

        #endregion VIEW_MEZZI

        #region MANUTENZIONE_UNITA_MISURA_USURA
        /// <summary>
        /// prelevo l'ultimo id del contatore (key primary) della tabella MANUTENZIONE_UNITA_MISURA_USURA
        /// </summary>
        /// <returns></returns>
        private int GetMaxManutenzioneUnitaMisuraUsura()
        {
            int iret = -1;
            try
            {
                truckmanageContext context = new truckmanageContext();
                var query = (from m in context.ManutenzioneUnitaMisuraUsura
                             select m.IdUmUsura).Max();
                iret = query;
            }
            catch (Exception oEx)
            {
                UtilityBE.AppLogger.Error("ERRORE nel metodo GetMaxManutenzioneUnitaMisuraUsura: " + oEx.Message);
            }
            return iret;
        }

        /// <summary>
        /// prelevo tutti gli elementi della tabella MANUTENZIONE_UNITA_MISURA_USURA
        /// </summary>
        /// <returns></returns>
        public IQueryable<ManutenzioneUnitaMisuraUsura> GetManutenzioneUnitaMisuraUsura()
        {
            try
            {
                truckmanageContext context = new truckmanageContext();

                var query = from m in context.ManutenzioneUnitaMisuraUsura
                            select m;

                return query.Take(Max);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// preleva tutti i tipi di manutenzione dalla tabella MANUTENZIONE_TIPO
        /// </summary>
        /// <returns></returns>
        public IQueryable<ManutenzioneTipo> GetManutenzioneTipo()
        {
            try
            {
                truckmanageContext context = new truckmanageContext();

                var query = from m in context.ManutenzioneTipo
                            select m;

                return query.Take(Max);
            }
            catch
            {
                return null;
            }
        }
        #endregion MANUTENZIONE_UNITA_MISURA_USURA

        #region AnagraficaMezzi
        /// <summary>
        /// prelevo l'ultimo id inserito della tabella AnagraficaMezzi
        /// </summary>
        /// <returns></returns>
        public int GetMaxAnagMezzi()
        {
            int iret = -1;
            try
            {
                truckmanageContext context = new truckmanageContext();
                var query = (from m in context.AnagraficaMezzi
                             select m.IdMezzo).Max();
                iret = query;
            }
            catch (Exception oEx)
            {
                AppLogger.Error("ERRORE nel metodo GetMaxAnagMezzi: " + oEx.Message);
                return 0;
            }
            return iret;
        }
        /// <summary>
        /// preleva tutti gli elementi dalla tabella AnagraficaMezzi
        /// </summary>
        /// <returns></returns>
        public IQueryable<AnagraficaMezzi> GetMezzi()
        {
            try
            {
                truckmanageContext context = new truckmanageContext();

                var query = from m in context.AnagraficaMezzi
                            orderby m.Targha
                            select m;

                return query.Take(Max);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// prelevo tutti gli elementi della tabella AnagraficaMezzi secondo la key in input
        /// </summary>
        /// <param name="IdKey"></param>
        /// <returns></returns>
        public IQueryable<AnagraficaMezzi> GetMezzi(int IdKey)
        {
            try
            {
                truckmanageContext context = new truckmanageContext();

                var query = from m in context.AnagraficaMezzi
                            where m.IdMezzo == IdKey
                            select m;

                return query;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// prelevo il corrispondente record della tabella AnagraficaMezzi per la targa data in input
        /// </summary>
        /// <param name="targa"></param>
        /// <returns></returns>
        public IQueryable<AnagraficaMezzi> GetMezzi(string targa)
        {
            try
            {
                truckmanageContext context = new truckmanageContext();

                var query = from m in context.AnagraficaMezzi
                            where m.Targha == targa
                            select m;

                return query;
            }
            catch
            {
                return null;
            }
        }

        public IQueryable<AnagraficaMezzi> GetMezzi(int idTipoMezzo, int idMarca)
        {
            try
            {
                truckmanageContext context = new truckmanageContext();

                var query = from m in context.AnagraficaMezzi
                            where m.IdTipoMezzo == idTipoMezzo &&
                                    m.IdMarca == idMarca
                            select m;

                return query;
            }
            catch
            {
                return null;
            }
        }

        public int UpdateAnagraficaMezzi(int IdMezzo, int idMarca, string modello, int cv, DateTime dtImmatricolazione, string targa,
                                        string telaio, string allestimento, string Note, int idTipoMezzo)
        {
            int iRet = 1;
            try
            {
                using (truckmanageContext context = new truckmanageContext())
                {

                    //prelevo un solo elemento in base alla chiave(quello da aggiornare)devo sempre usare il contex!                  
                    var Am = context.AnagraficaMezzi.Where(c => c.IdMezzo == IdMezzo).First();

                    //aggiorno tutti i campi dell'oggetto 
                    Am.IdMezzo = IdMezzo;
                    Am.IdMarca = idMarca;
                    Am.Modello = modello;
                    Am.Cv = cv;
                    Am.DataImm = dtImmatricolazione;
                    Am.Targha = targa;
                    Am.Telaio = telaio;
                    Am.Allestimento = allestimento;
                    Am.Note = Note;
                    Am.IdTipoMezzo = idTipoMezzo;

                    //rendo ufficiali le modifiche anche sul db
                    context.SaveChanges();
                }
            }
            catch (Exception oEx)
            {
                AppLogger.Error("ERRORE nel metodo UpdateAnagraficaMezzi: " + oEx.Message);
                return -1;
            }
            return iRet;
        }

        public int SaveNewAnagraficaMezzi(int idmarca, string modello, int cv, DateTime dtImmatricolazione, string targa,
                                       string telaio, string allestimento, string note, int idtipomezzo)
        {
            int iRet = 1;
            try
            {
                //prelevo il max della key della tabella
                int iMaxIdAnagMezzi = this.GetMaxAnagMezzi();

                using (truckmanageContext context = new truckmanageContext())
                {
                    AnagraficaMezzi Am = new AnagraficaMezzi
                    {
                        //aggiorno tutti i campi dell' 
                        IdMezzo = iMaxIdAnagMezzi + 1,
                        IdMarca = idmarca,
                        Modello = modello,
                        Cv = cv,
                        DataImm = dtImmatricolazione,
                        Targha = targa,
                        Telaio = telaio,
                        Allestimento = allestimento,
                        Note = note,
                        IdTipoMezzo = idtipomezzo
                    };

                    //salvo definitivamente sul db
                    context.AnagraficaMezzi.Add(Am);
                    context.SaveChanges();
                    iRet = 1;
                }
            }
            catch (Exception oEx)
            {
                AppLogger.Error("ERRORE nel metodo SaveNewAnagraficaMezzi: " + oEx.Message);
                return -1;
            }
            return iRet;
        }

        /// <summary>
        /// modifica solo il valore della Targha
        /// </summary>
        /// <param name="IdMezzo"></param>
        /// <param name="Targha_old"></param>
        /// <param name="Targha_new"></param>
        /// <returns></returns>
        public int UpdateAnagraficaMezzi_Targha(string Targha_old, string Targha_new)
        {
            int iRet = 1;
            try
            {
                using (truckmanageContext context = new truckmanageContext())
                {

                    //prelevo un solo elemento in base alla chiave(quello da aggiornare)devo sempre usare il contex!                  
                    var Am = context.AnagraficaMezzi.Where(c => c.Targha == Targha_old).First();

                    //aggiorno tutti i campi dell'oggetto 
                    Am.Targha = Targha_new;

                    //rendo ufficiali le modifiche anche sul db
                    context.SaveChanges();
                }
            }
            catch (Exception oEx)
            {
                AppLogger.Error("ERRORE nel metodo UpdateAnagraficaMezzi_Targha: " + oEx.Message);
                return -1;
            }
            return iRet;
        }


        #endregion AnagraficaMezzi

        #region AnagraficaMezziTipo
        /// <summary>
        /// preleva il max della colonna ID della tabella AnagraficaMezziTipo
        /// </summary>
        /// <returns></returns>
        private int GetMaxAnagMezziTipo()
        {
            int iret = -1;
            try
            {
                truckmanageContext context = new truckmanageContext();
                var query = (from m in context.AnagraficaMezziTipo
                             select m.IdTipoMezzo).Max();
                iret = query;
            }
            catch (Exception oEx)
            {
                AppLogger.Error("ERRORE nel metodo GetMaxAnagMezziTipo: " + oEx.Message);
                return 0;
            }
            return iret;
        }

        /// <summary>
        /// preleva tutti i valori della tabella AnagraficaMezziTipo
        /// </summary>
        /// <returns></returns>
        public IQueryable<AnagraficaMezziTipo> GetTipoMezzi()
        {
            try
            {
                truckmanageContext context = new truckmanageContext();

                var query = from m in context.AnagraficaMezziTipo
                            orderby m.TipoMezzo
                            select m;

                return query.Take(Max);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// preleva tutti i valori della tabella AnagraficaMezziTipo secondo la key in input
        /// </summary>
        /// <param name="idTipoMezzi"></param>
        /// <returns></returns>
        public IQueryable<AnagraficaMezziTipo> GetTipoMezzi(int idTipoMezzi)
        {
            try
            {
                truckmanageContext context = new truckmanageContext();

                var query = from m in context.AnagraficaMezziTipo
                            where m.IdTipoMezzo == idTipoMezzi
                            select m;

                return query.Take(Max);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// preleva tutti i valori della tabella AnagraficaMezziTipo secondo la key in input
        /// </summary>
        /// <param name="tipoMezzi"></param>
        /// <returns></returns>
        public IQueryable<AnagraficaMezziTipo> GetTipoMezzi(string tipo)
        {
            try
            {
                truckmanageContext context = new truckmanageContext();

                var query = from m in context.AnagraficaMezziTipo
                            where m.TipoMezzo == tipo
                            select m;

                return query.Take(Max);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Inserisce nella tabella AnagraficaMezziTipo un nuovo elemento
        /// </summary>
        /// <param name="newtipo"></param>
        /// <returns></returns>
        public int SaveNewTipoMezzo(string newtipo)
        {
            int iRet = -1;
            try
            {
                //prelevo il max della key della tabella
                int iMaxIdAnagMezziTipo = this.GetMaxAnagMezziTipo();

                //creo il nuovo elemento del contex
                using (truckmanageContext context = new truckmanageContext())
                {
                    var NewTipoMezzo = new AnagraficaMezziTipo
                    {
                        IdTipoMezzo = iMaxIdAnagMezziTipo + 1,
                        TipoMezzo = newtipo
                    };

                    //salvo definitivamente sul db
                    context.AnagraficaMezziTipo.Add(NewTipoMezzo);
                    context.SaveChanges();
                    iRet = 1;
                }
            }
            catch (Exception oEx)
            {
                AppLogger.Error("ERRORE nel metodo SaveNewTipoMezzo: " + oEx.Message);
                return -1;
            }
            return iRet;
        }

        /// <summary>
        /// aggiorna un tipo mezzo esistente
        /// </summary>
        /// <param name="tipo_old"></param>
        /// <param name="tipo_new"></param>
        /// <returns></returns>
        public int UpdateTipoMezzo(string tipo_old, string tipo_new)
        {
            int iRet = 1;
            try
            {
                using (truckmanageContext context = new truckmanageContext())
                {

                    //prelevo un solo elemento in base alla chiave(quello da aggiornare)devo sempre usare il contex!                  
                    var Am = context.AnagraficaMezziTipo.Where(c => c.TipoMezzo == tipo_old).First();

                    //aggiorno tutti i campi dell'oggetto 
                    Am.TipoMezzo = tipo_new;

                    //rendo ufficiali le modifiche anche sul db
                    context.SaveChanges();
                }
            }
            catch (Exception oEx)
            {
                AppLogger.Error("ERRORE nel metodo UpdateTipoMezzo: " + oEx.Message);
                return -1;
            }
            return iRet;
        }

        #endregion AnagraficaMezziTipo

        #region AnagraficaMezziMarca
        /// <summary>
        /// preleva l'ultimo valore della colonna id nella tabella AnagraficaMezziMarca
        /// </summary>
        /// <returns></returns>
        private int GetMaxAnagraficaMezziMarca()
        {
            int iret = -1;
            try
            {
                truckmanageContext context = new truckmanageContext();
                var query = (from m in context.AnagraficaMezziMarca
                             select m.IdMarca).Max();
                iret = query;
            }
            catch (Exception oEx)
            {
                UtilityBE.AppLogger.Error("ERRORE nel metodo GetMaxAnagraficaMezziMarca: " + oEx.Message);
            }
            return iret;
        }

        /// <summary>
        /// preleva tutti gli elementio della tabella AnagraficaMezziMarca
        /// </summary>
        /// <returns></returns>
        public IQueryable<AnagraficaMezziMarca> GetMarcaMezzi()
        {
            try
            {
                truckmanageContext context = new truckmanageContext();

                var query = from m in context.AnagraficaMezziMarca
                            orderby m.Marca
                            select m;

                return query.Take(Max);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// preleva tutti i valori della tabella AnagraficaMezziMarca secondo il filtro in input sulla key 
        /// </summary>
        /// <param name="idMarca"></param>
        /// <returns></returns>
        public IQueryable<AnagraficaMezziMarca> GetMarcaMezzi(int idMarca)
        {
            try
            {
                truckmanageContext context = new truckmanageContext();

                var query = from m in context.AnagraficaMezziMarca
                            where m.IdMarca == idMarca
                            select m;

                return query.Take(Max);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// preleva tutti i valori della tabella AnagraficaMezziMarca secondo il filtro in input
        /// </summary>
        /// <param name="marca"></param>
        /// <returns></returns>
        public IQueryable<AnagraficaMezziMarca> GetMarcaMezzi(string marca)
        {
            try
            {
                truckmanageContext context = new truckmanageContext();

                var query = from m in context.AnagraficaMezziMarca
                            where m.Marca == marca
                            select m;

                return query.Take(Max);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// inserisce nella tabella AnagraficaMezziMarca un nuovo elemento
        /// </summary>
        /// <param name="newmarca"></param>
        /// <returns></returns>
        public int SaveNewMarcaMezzi(string newmarca, string Note)
        {
            int iRet = -1;
            try
            {
                int iMaxId = this.GetMaxAnagraficaMezziMarca();

                //creo il nuovo elemento del contex
                using (truckmanageContext context = new truckmanageContext())
                {
                    var NewMarcaMezzo = new AnagraficaMezziMarca
                    {
                        IdMarca = iMaxId + 1,
                        Marca = newmarca,
                        Note = Note
                    };

                    //salvo definitivamente sul db
                    context.AnagraficaMezziMarca.Add(NewMarcaMezzo);
                    context.SaveChanges();
                    iRet = 1;
                }
            }
            catch (Exception oEx)
            {
                AppLogger.Error("ERRORE nel metodo SaveNewMarcaMezzi: " + oEx.Message);
                return -1;
            }
            return iRet;
        }

        /// <summary>
        /// aggiorna una marca esistente
        /// </summary>
        /// <param name="marca_old"></param>
        /// <param name="marca_new"></param>
        /// <returns></returns>
        public int UpdateMarcaMezzi(string marca_old, string marca_new, string Note)
        {
            int iRet = 1;
            try
            {
                using (truckmanageContext context = new truckmanageContext())
                {

                    //prelevo un solo elemento in base alla chiave(quello da aggiornare)devo sempre usare il contex!                  
                    var Am = context.AnagraficaMezziMarca.Where(c => c.Marca == marca_old).First();

                    //aggiorno tutti i campi dell'oggetto 
                    Am.Marca = marca_new;
                    Am.Note = Note;

                    //rendo ufficiali le modifiche anche sul db
                    context.SaveChanges();
                }
            }
            catch (Exception oEx)
            {
                AppLogger.Error("ERRORE nel metodo UpdateMarcaMezzi: " + oEx.Message);
                return -1;
            }
            return iRet;
        }
        #endregion AnagraficaMezziMarca

        #region ManutenzioneMagazzino
        /// <summary>
        /// preleva il max numero della id della tabella ManutenzioneMagazzino
        /// </summary>
        /// <returns></returns>
        public int GetMaxManMag()
        {
            int iret = -1;
            try
            {
                using (truckmanageContext context = new truckmanageContext())
                {
                    var query = (from m in context.ManutenzioneMagazzino
                                 select m.IdMagazzinoManutenzione).Max();
                    iret = query;
                }
            }
            catch (Exception oEx)
            {
                UtilityBE.AppLogger.Error("ERRORE nel metodo GetMaxManMag: " + oEx.Message);
            }
            return iret;
        }

        /// <summary>
        /// Preleva tutte righe della tabella ManutenzioneMagazzino per il filtro in input dell ID MAGAZZINO
        /// </summary>
        /// <param name="idMagazzino"></param>
        /// <returns></returns>
        public IQueryable<ManutenzioneMagazzino> GetManutenzioneMagazzino(int idMagazzino)
        {
            try
            {
                truckmanageContext context = new truckmanageContext();

                var query = from m in context.ManutenzioneMagazzino
                            where m.IdMagazzino == idMagazzino
                            select m;

                return query.Take(Max);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// prelevas tutti gli elementi della tabella ManutenzioneMagazzino secondi il filtro in ingresso
        /// </summary>
        /// <param name="idManutenzione"></param>
        /// <returns></returns>
        public IQueryable<ManutenzioneMagazzino> GetManutenzioneMagazzinoToIdManutenzione(int idManutenzione)
        {
            try
            {
                truckmanageContext context = new truckmanageContext();

                var query = from m in context.ManutenzioneMagazzino
                            where m.IdManutenzione == idManutenzione
                            select m;

                return query.Take(Max);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// prelevo tutto dalla vista ViewManutenzioneMateriale secondo il filtro idManutenzione
        /// </summary>
        /// <param name="idManutenzione"></param>
        /// <returns></returns>
        public IQueryable<ViewManutenzioneMateriale> GetViewManMateriale(int idManutenzione)
        {
            try
            {
                truckmanageContext context = new truckmanageContext();

                var query = from m in context.ViewManutenzioneMateriale
                            where m.IdManutenzione == idManutenzione
                            select m;

                return query;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// salva un nuovo record nella tabella MAGAZZINO_MANUTENZIONE
        /// </summary>
        /// <param name="idMagazzinoManutenzione"></param>
        /// <param name="IdMagazzino"></param>
        /// <param name="idManutenzione"></param>
        /// <param name="quantita"></param>
        /// <returns></returns>
        public int SaveNewManutenzioneMagazzino(int IdMagazzino, int idManutenzione, int quantita)
        {
            int iRet = -1;
            try
            {
                //prelevo il max della key della tabella
                int iMaxIdMag = this.GetMaxManMag();

                //creo il nuovo elemento del contex
                using (truckmanageContext context = new truckmanageContext())
                {
                    var NewManutenzioneMagazzino = new ManutenzioneMagazzino
                    {
                        IdMagazzinoManutenzione = iMaxIdMag + 1,
                        IdMagazzino = IdMagazzino,
                        IdManutenzione = idManutenzione,
                        Quantita = quantita
                    };

                    //salvo definitivamente sul db
                    context.ManutenzioneMagazzino.Add(NewManutenzioneMagazzino);
                    context.SaveChanges();
                    iRet = 1;
                }
            }
            catch (Exception oEx)
            {
                AppLogger.Error("ERRORE nel metodo SaveNewManMag: " + oEx.Message);
                return -1;
            }
            return iRet;
        }

        /// <summary>
        /// aggiorna un campo della tabella ManutenzioneMagazzino
        /// </summary>
        /// <param name="idMagazzinoManutenzione"></param>
        /// <param name="IdMagazzino"></param>
        /// <param name="idManutenzione"></param>
        /// <param name="quantita"></param>
        /// <returns></returns>
        public int UpdateManutenzioneMagazzino(int idMagazzinoManutenzione, int IdMagazzino, int idManutenzione, int quantita)
        {
            int iRet = 1;
            try
            {
                using (truckmanageContext context = new truckmanageContext())
                {

                    //prelevo un solo elemento in base alla chiave(quello da aggiornare)devo sempre usare il contex!                  
                    var Mm = context.ManutenzioneMagazzino.Where(c => c.IdMagazzinoManutenzione == idMagazzinoManutenzione).First();

                    //aggiorno tutti i campi dell'oggetto 
                    Mm.IdMagazzinoManutenzione = idMagazzinoManutenzione;
                    Mm.IdMagazzino = IdMagazzino;
                    Mm.IdManutenzione = idManutenzione;
                    Mm.Quantita = quantita;

                    //rendo ufficiali le modifiche anche sul db
                    context.SaveChanges();
                }
            }
            catch (Exception oEx)
            {
                AppLogger.Error("ERRORE nel metodo UpdateManutenzioneMagazzino: " + oEx.Message);
                return -1;
            }
            return iRet;
        }

        /// <summary>
        /// cancello items dalla tabella ManutenzioneMagazzino secondo il filtro ID_MAGAZZINO e IdManutenzione
        /// </summary>
        /// <param name="idmanutenzione"></param>
        /// <param name="IdMagazzino"></param>
        /// <returns></returns>
        public int DeleteLavorazioneMateriale(int idmanutenzione, int IdMagazzino)
        {
            int iRet = -1;
            try
            {
                using (truckmanageContext context = new truckmanageContext())
                {
                    var query = from m in context.ManutenzioneMagazzino
                                where m.IdMagazzino == IdMagazzino
                                && m.IdManutenzione == idmanutenzione
                                select m;

                    foreach (ManutenzioneMagazzino m in query)
                        context.ManutenzioneMagazzino.Remove(m);

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

        /// <summary>
        /// cancello items dalla tabella ManutenzioneMagazzino secondo il filtro IdManutenzione
        /// </summary>
        /// <param name="idmanutenzione"></param>
        /// <returns></returns>
        public static int DeleteLavorazioneMateriale(int idmanutenzione)
        {
            int iRet = -1;
            try
            {
                using (truckmanageContext context = new truckmanageContext())
                {
                    var query = from m in context.ManutenzioneMagazzino
                                where m.IdManutenzione == idmanutenzione
                                select m;

                    foreach (ManutenzioneMagazzino m in query)
                        context.ManutenzioneMagazzino.Remove(m);

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
        #endregion ManutenzioneMagazzino

        #region VISUALIZZA_LIBRETTO
        /// <summary>
        /// prelevo l'ultimo id inserito della tabella VISUALIZZA_LIBRETTO
        /// </summary>
        /// <returns></returns>
        public int GetMaxVisualizzaLibretto()
        {
            int iret = -1;
            try
            {
                truckmanageContext context = new truckmanageContext();
                var query = (from m in context.VisualizzaLibretto
                             select m.IdVisualizzaLibretto).Max();
                iret = query;
            }
            catch (Exception oEx)
            {
                AppLogger.Error("ERRORE nel metodo GetMaxVisualizzaLibretto: " + oEx.Message);
                return 0;
            }
            return iret;
        }
        /// <summary>
        /// preleva tutti gli elementi dalla tabella VISUALIZZA_LIBRETTO
        /// </summary>
        /// <returns></returns>
        public IQueryable<VisualizzaLibretto> GetVisualizzaLibretti()
        {
            try
            {
                truckmanageContext context = new truckmanageContext();

                var query = from m in context.VisualizzaLibretto
                            orderby m.IdMezzo
                            select m;

                return query.Take(Max);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// prelevo tutti gli elementi della tabella VISUALIZZA_LIBRETTO secondo lo ID mezzo
        /// </summary>
        /// <param name="IdKey"></param>
        /// <returns></returns>
        public IQueryable<VisualizzaLibretto> GetVisualizzaLibrettiToIdMezzo(int IdMezzo)
        {
            try
            {
                truckmanageContext context = new truckmanageContext();

                var query = from m in context.VisualizzaLibretto
                            where m.IdMezzo == IdMezzo
                            select m;

                return query;
            }
            catch
            {
                return null;
            }
        }

        public int UpdateVisualizzaLibretto(int idVisualizzaLibretto, int IdMezzo, string path, string Note)
        {
            int iRet = 1;
            try
            {
                using (truckmanageContext context = new truckmanageContext())
                {

                    //prelevo un solo elemento in base al IdMezzo(quello da aggiornare)devo sempre usare il contex!                  
                    var Vl = context.VisualizzaLibretto.Where(c => c.IdMezzo == IdMezzo).First();

                    //aggiorno tutti i campi dell'oggetto 
                    Vl.IdMezzo = IdMezzo;
                    Vl.IdVisualizzaLibretto = idVisualizzaLibretto;
                    Vl.Note = Note;
                    Vl.Path = path;

                    //rendo ufficiali le modifiche anche sul db
                    context.SaveChanges();
                }
            }
            catch (Exception oEx)
            {
                AppLogger.Error("ERRORE nel metodo UpdateVisualizzaLibretto: " + oEx.Message);
                return -1;
            }
            return iRet;
        }

        public int SaveNewVisualizzaLibretto(int IdMezzo, string path, string Note)
        {
            int iRet = 1;
            try
            {
                //prelevo il max della key della tabella
                int iMaxIdVisualizzaLibretto = this.GetMaxVisualizzaLibretto();

                using (truckmanageContext context = new truckmanageContext())
                {
                    VisualizzaLibretto Am = new VisualizzaLibretto
                    {
                        //aggiorno tutti i campi dell' 
                        IdMezzo = IdMezzo,
                        IdVisualizzaLibretto = iMaxIdVisualizzaLibretto + 1,
                        Path = path,
                        Note = Note
                    };

                    //salvo definitivamente sul db
                    context.VisualizzaLibretto.Add(Am);
                    context.SaveChanges();
                    iRet = 1;
                }
            }
            catch (Exception oEx)
            {
                AppLogger.Error("ERRORE nel metodo SaveNewVisualizzaLibretto: " + oEx.Message);
                return -1;
            }
            return iRet;
        }


        public int DeleteVisualizzazioneLibretto(int IdKey)
        {
            int iRet = -1;
            try
            {
                using (truckmanageContext context = new truckmanageContext())
                {
                    var query = from m in context.VisualizzaLibretto
                                where m.IdVisualizzaLibretto == IdKey
                                select m;

                    foreach (VisualizzaLibretto m in query)
                        context.VisualizzaLibretto.Remove(m);

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
        #endregion VISUALIZZA_LIBRETTO

        /// <summary>
        /// cancello items dalla tabella ManutenzioneMagazzino secondo il filtro IdManutenzione e ID_MAGAZZINO
        /// </summary>
        /// <param name="idLavorazione"></param>
        /// <param name="IdMagazzino"></param>
        /// <returns></returns>
        public int DeleteManutenzioneMateriale(int idManutenzione, int IdItem)
        {
            int iRet = -1;
            try
            {
                using (truckmanageContext context = new truckmanageContext())
                {
                    var query = from m in context.ManutenzioneMagazzino
                                where m.IdMagazzino == IdItem
                                && m.IdManutenzione == idManutenzione
                                select m;

                    foreach (ManutenzioneMagazzino m in query)
                        context.ManutenzioneMagazzino.Remove(m);

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
    }
}
