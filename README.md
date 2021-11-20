# TS Readme Generator
A program that not only packages TS scenarios efficiently and quickly into a folder, but also automatically writes the scenario README based on scenario properties and user input.

## Basics
![TS_ReadMe_Maker_VciTSBXBbx](https://user-images.githubusercontent.com/58154576/142740940-0d5eb9b8-fc27-4c8d-b8fd-14f248b7072d.png)

The Scenario must be a TS scenario folder, example: C:\Program Files (x86)\Steam\steamapps\common\RailWorks\Content\Routes\00000038-0000-0000-0000-000000002013\Scenarios\f56b7ba8-2188-4032-bf5e-807690923847

You can get the location of one for a scenario through the Build menu on the main screen of TS as long as your game is in either Windowed or Borderless mode.

All elements on this tab are required, including the scenario image.

## Details
![TS_ReadMe_Maker_SBYGTvIt2E](https://user-images.githubusercontent.com/58154576/142740972-f4ee9068-0aed-4b2f-9d10-8bb528728bcc.png)

Name, Description, Briefing, Length, and Difficulty are automatically filled, if applicable, when selecting a scenario folder. Conditions are not filled but this is an optional field.

## Requirements
![TS_ReadMe_Maker_KysLCAQOxN](https://user-images.githubusercontent.com/58154576/142741015-4813f20c-5d0c-4dbf-adb2-bebdb1a0c273.png)

This table is where all items utilized in the scenario go. Writing anything in an empty row will automatically create a new row for the next requirement. Empty rows are excluded from the generation of the README.

The following developer abbreviations will be enlarged in the final readme: AP, JT, ATS, MJW, DPS, OTS, VP, UKTS.

Additionally, EP if seen in any individual requirement is enlarged to "Enhancement Pack", SP to "Sound Pack", "(C)" is Cummins and "(P)" is Perkins.

**Provider** - Who is this product distributed by? (Armstrong Powerhouse, Steam, Just Trains, Major Wales Design...)

**Product** - What is the name of this product?

**Freeware?** - If this product is free and can be downloaded without paying, enter Y. If you need to pay for it to legitimately obtain it enter N.

**Required?** - If the player does not have this item installed, and as such the scenario will NOT still run as intended, enter Y. Else, enter N.

**Notes** - Optional field that can be used to list notes about each requirement, such as how they are used or if any extensions to them are also required

## Easy Copy and Paste
![TS_ReadMe_Maker_47LfXyfRGm](https://user-images.githubusercontent.com/58154576/142741044-b9a12cd4-7cfb-42d9-86b1-7d9a4cb1d0c1.png)

This tab is present for ease of pasting in a large number of requirements. Once "Paste into Requirement List" is clicked, each line in the "All Items" list will be inserted into the requirements list on the previous tab, under the "Provider Name" specified.

Adding * to the end of an item makes it an optional item. Otherwise it will be a required item.

If the provider name is Steam/DTG/Dovetail Games/AP/Armstrong Powerhouse/JT/Just Trains it will automatically be listed as payware.

If the provider name is MJW/Major Wales Design/DPS/DPSimulation/UKTS/UKTrainSim, it will be automatically listed as freeware.

## Other Things
- The generated readme will be in .HTML format. You can modify the HTML after generation using a dedicated HTML editor such as Notepad++ if you wish. Afterwards, you can use third party tool to convert it to a PDF if you wish, though some weird things may occur depending on what converter you use.

