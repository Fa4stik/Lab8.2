﻿using PIS8_2.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIS8_2.Converters
{
    internal static class  ConverterCardsToLimitedCards
    {
        public static List<LimitedCard> ConvertCardsToLimitedCards(IEnumerable<Card> cards)
        {
            return cards.Select(card => new LimitedCard(card.Id, card.Nummk, card.Datemk, card.IdMunicipNavigation.Namemunicip, card.IdOmsuNavigation.Nameomsu, card.Numworkorder, card.Locality, card.Dateworkorder, card.Datetrapping, card.Targetorder, card.TypeOrder,card.IdOrgNavigation.Nameorg)).ToList();
        }
    }
}