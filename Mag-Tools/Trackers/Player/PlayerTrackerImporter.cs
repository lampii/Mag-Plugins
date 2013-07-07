﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace MagTools.Trackers.Player
{
	class PlayerTrackerImporter
	{
		readonly string fileName;

		public PlayerTrackerImporter(string fileName)
		{
			this.fileName = fileName;
		}

		public bool Import(List<TrackedPlayer> trackedPlayers)
		{
			FileInfo fileInfo = new FileInfo(fileName);

			if (!fileInfo.Exists)
				return false;

			XmlDocument xmlDocument = new XmlDocument();

			xmlDocument.Load(fileName);

			XmlNode playersNode = xmlDocument.SelectSingleNode("Players");

			if (playersNode == null)
				return false;

			trackedPlayers.Clear();

			// Import the Players
			if (playersNode.HasChildNodes)
			{
				foreach (XmlNode node in playersNode.ChildNodes)
				{
					if (node.Attributes == null || node.Attributes.Count == 0)
						continue;

					XmlAttribute attribute;

					TrackedPlayer item;

					if ((attribute = node.Attributes["Name"]) != null)
						item = new TrackedPlayer(attribute.Value);
					else
						continue;

					if ((attribute = node.Attributes["LastSeen"]) != null)
					{
						long value;
						if (long.TryParse(attribute.Value, out value))
							item.LastSeen = new DateTime(value);
					}

					if ((attribute = node.Attributes["LandBlock"]) != null)
					{
						int value;
						if (int.TryParse(attribute.Value, out value))
							item.LandBlock = value;
					}

					if ((attribute = node.Attributes["LocationX"]) != null)
					{
						double value;
						if (double.TryParse(attribute.Value, out value))
							item.LocationX = value;
					}

					if ((attribute = node.Attributes["LocationY"]) != null)
					{
						double value;
						if (double.TryParse(attribute.Value, out value))
							item.LocationY = value;
					}

					if ((attribute = node.Attributes["LocationZ"]) != null)
					{
						double value;
						if (double.TryParse(attribute.Value, out value))
							item.LocationZ = value;
					}

					trackedPlayers.Add(item);
				}
			}

			return true;
		}
	}
}
