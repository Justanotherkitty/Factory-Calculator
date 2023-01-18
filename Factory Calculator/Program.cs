using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Factory_Calculator
{
	public class Item
	{
		public string ItemName { get; set; }
		public Dictionary<String, int> Ingredients { get; set; }
		public float? Craft_Time { get; set; }
		public int? Amount { get; set; }
	}

	class Program
	{
		public static Dictionary<string, Item> Items { get; set; }
		public static Dictionary<string, float> ItemFactoryTotals { get; set; }

		static void Main(string[] args)
		{
			ItemFactoryTotals = new Dictionary<string, float>();

			string filename = args.Length == 0 ? "settings.json" : args[0];

			if (File.Exists(filename))
			{
				string jsonText = File.ReadAllText(filename);
				try
				{
					Items = JsonSerializer.Deserialize<Dictionary<string, Item>>(jsonText)!;

					Console.WriteLine("What do you want to craft?");
					string itemToCraft = Console.ReadLine().Trim().ToLower();

					Calculate(Items[itemToCraft]);
				}
				catch (Exception e)
				{
					Console.WriteLine(e);
					Environment.Exit(1);
				}
			}
			else
			{
				Console.WriteLine("File does not exist! (default file name: settings.json)");
				Environment.Exit(1);
			}
        }

		static void Calculate(Item itemToCraft, bool init = true)
        {
			foreach(KeyValuePair<string, int> kvp in itemToCraft.Ingredients)
            {
				if (Items[kvp.Key].Ingredients != null)
				{
					Item crafting = Items[kvp.Key];
					float ratio = (float)((kvp.Value * crafting.Craft_Time)/ crafting.Amount / itemToCraft.Craft_Time);

                    if (ItemFactoryTotals.ContainsKey(kvp.Key))
                    {
						ItemFactoryTotals[kvp.Key] += ratio;
                    }
                    else
                    {
						ItemFactoryTotals.Add(kvp.Key, ratio);
                    }

					Calculate(crafting, false);
				}
            }

            if (init)
            {
				foreach (KeyValuePair<string, float> kvp in ItemFactoryTotals)
				{
					Console.WriteLine($"{kvp.Key} machines needed: {MathF.Ceiling(kvp.Value)}");
				}
				Console.WriteLine("\nPress Any Key To Continue...");
				Console.ReadKey();
			}
        }

    }
}
