﻿using MoneyKeeper.Budget.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyKeeper.Console.GCloud
{
    internal class BudgetCategoriesGenerator
    {
        private IGoogleDocsEditor _googleDocsEditor;

        public BudgetCategoriesGenerator(IGoogleDocsEditor googleDocsEditor)
        {
            _googleDocsEditor = googleDocsEditor;
        }

        internal IReadOnlyCollection<BudgetCategory> Generate(string range)
        {
            var rawData = _googleDocsEditor.GetValuesRange(range);
            var categories = new List<BudgetCategory>();
            var groupBy = string.Empty;
            var assignNewGroup = true;
            foreach (var rawItem in rawData)
            {
                if (rawItem == ".")
                    continue;

                if (rawItem == string.Empty || rawItem == " ")
                {
                    assignNewGroup = true;
                    continue;
                }

                if(assignNewGroup)
                {
                    groupBy = rawItem;
                    assignNewGroup = false;
                    continue;
                }

                categories.Add(new BudgetCategory
                {
                    Group = groupBy,
                    Category = rawItem,
                    Comment = string.Empty
                });
            }

            return categories;
        }
    }
}