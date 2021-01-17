using System.Collections;
using System.IO;
using System.Linq.Expressions;
using System;
using System.Collections.Generic;
using TRUCKMANAGEMENT.Models.ViewModels;

namespace TRUCKMANAGEMENT.Models.Services.Application
{
    public class MagazzinoService
    {
        public List<MagazzinoViewModel> GetMagazzinoLista()
        {

            var magazzinoList = new List<MagazzinoViewModel>();

            var rand = new Random();
            for (int i = 0; i <= 20; i++)
            {
                var mag = new MagazzinoViewModel
                {
                
                    IdMagazzino = i,
                    NomeArticolo = "nome dell'articolo",
                    PrezzoUnitario = Convert.ToDecimal(rand.NextDouble() * 10 + 10),
                    NomeFornitore = "franzini Annibale",
                    CodiceArticolo = "codice articolo",
                    Quantita = 20,
                    QuantitaMin = 2
                };
                magazzinoList.Add(mag);
            }
            return magazzinoList;
        }

        public MagazzinoDettaglioViewModel GetMagazzinoDettaglio(string id)
        {
            var magElement = new MagazzinoDettaglioViewModel
            {
                Categoria = "categoria_esempio",
                CodiceBarre = "345463456435645674567456745",               
                IdArticolo = "idarticolo",
                IdMagazzino = 23,
                CodiceArticolo = "codice dell'articolo",
                NomeArticolo = "nome dell'articolo(in realta è la descrizione)",
                Quantita = 20,
                QuantitaMin = 3,
                UnitaMisura = "N°",
                Iva = 22,
                NomeFornitore = "nome fornitore",
                Marca = "marca del veicolo associato al pezzo",
                PrezzoUnitario = 100,
                Note = "note eventuali",
                ImagePath = "percorso immagine",
                FlagScarico = true
            };
            return magElement;
        }
    }
}
