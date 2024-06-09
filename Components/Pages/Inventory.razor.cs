using System.Reflection.Metadata;
using Microsoft.VisualBasic;

namespace BlazorApp3.Components.Pages
{
    public partial class Inventory
    {
        private List<Medication> medications = new();
        private Medication newMedication = new Medication();
        private string? newName = "";
        private int? newQuantity = null;
        private long? newNDC = null;
        private int? newStrength = null;
        private bool itemExists = false;
        private bool? existingNDC = false;
        private bool itemsEntered = false;
        private bool editItem = false;
        private bool ndcValidLength = true;
        private bool inventoryMenu = false;
        private bool inputEmpty = false;
        private int i = 0;
        private int editingIndex = -1;
        private string? newFrom = "";
        private string? newStrengthVolume = "";
        private string? newMassStrength = "";

        private void MenuActivated()
        {
            inventoryMenu = true;
        }
        private void MenuDeactivated()
        {
            inventoryMenu = false;
            ClearAlertMessage();
        }
        private void EditingItem(int index)
        {
            editingIndex = index;
            editItem = true;
        }

        private void ClearAlertMessage()
        {
            itemExists = false;
            existingNDC = false;
            itemsEntered = false;
            inputEmpty = false;
        }

        private void ClearEntries()
        {
            newName = "";
            newQuantity = null;
            newNDC = null;
            newStrength = null;
        }

        private void ResetAlert()
        {
            itemExists = false;
            existingNDC = false;
            itemsEntered = false;
        }

        void SaveTodo(int index)
        {
            editItem = false;
            ResetAlert();

            if (index >= 0 && index < medications.Count && !string.IsNullOrWhiteSpace(newName) && !string.IsNullOrWhiteSpace(newNDC.ToString()))
            {
                // Check if the medication already exists
                bool ndcExists = medications.Any(m => m.NDC == newNDC);
                bool strengthExists = medications.Any(m => m.Strength == newStrength);
                bool nameExists = medications.Any(m => string.Equals(m.Name, newName, StringComparison.OrdinalIgnoreCase));

                if (ndcExists && nameExists && strengthExists)
                {
                    // Medication already exists, increase quantity
                    itemExists = true;
                    medications[index].Quantity += newQuantity ?? 0; // Add the newQuantity if not null
                    ClearEntries(); // Clear inputs
                }
                else if (ndcExists && nameExists)
                {
                    // Set flag indicating NDC exists, prevent adding new medication name
                    existingNDC = true;
                    ClearEntries();
                }
                else if (ndcExists)
                {
                    // Set flag indicating NDC exists, prevent adding new medication name
                    existingNDC = true;
                    ClearEntries();
                }
                else
                {
                    // Update medication details
                    medications[index].Name = newName;
                    medications[index].NDC = newNDC;
                    medications[index].Strength = newStrength;
                    medications[index].Quantity = newQuantity ?? 0;
                    medications[index].Form = newFrom;

                    ClearEntries(); // Clear inputs
                    itemsEntered = true;
                }
            }
            else
            {
                // Handle index out of range error or other invalid index scenarios
                Console.WriteLine("Invalid index or medication not found.");
                ClearAlertMessage();
            }
        }

        private void AddTodo()
        {
            Console.WriteLine(newNDC?.ToString().Length);
            editItem = false;
            ResetAlert();
            if (!string.IsNullOrWhiteSpace(newName) && newNDC.HasValue)
            {
                // Check if the medication already exists

                bool ndcExists = medications.Any(m => m.NDC == newNDC);
                bool strengthExists = medications.Any(m => m.Strength == newStrength);
                bool nameExists = medications.Any(m => string.Equals(m.Name, newName, StringComparison.OrdinalIgnoreCase));

                // Check if the medication already exists
                if (ndcExists && nameExists && strengthExists)
                {
                    int j = medications.FindIndex(m => m.Name == newName);
                    // Medication already exists, increase quantity
                    itemExists = true;
                    medications[j].Quantity += newQuantity ?? 0; // Add the newQuantity if not null
                    ClearEntries(); // Clear inputs
                }
                else if (ndcExists && nameExists)
                {
                    // Set flag indicating NDC exists, prevent adding new medication name
                    existingNDC = true;
                    ClearEntries();
                }
                else if (ndcExists)
                {
                    // Set flag indicating NDC exists, prevent adding new medication name
                    existingNDC = true;
                    ClearEntries();
                }
                else if (newNDC.ToString().Length != 11 && newNDC.ToString().Length != 10)
                {
                    ndcValidLength = false;
                    ClearEntries();
                }
                else
                {
                    // Medication doesn't exist, add new medication
                    medications.Add(new Medication { Name = newName.ToUpper(), NDC = newNDC, Quantity = newQuantity ?? 0, Strength = newStrength, Form = newFrom, VolumeStrength = newStrengthVolume, StrengthMass = newMassStrength }); // Use newQuantity if not null
                    ClearEntries(); // Clear inputs
                    itemsEntered = true;
                    ndcValidLength = true;
                }
                editItem = false;
                inventoryMenu = false;
            }
            else
            {
                inputEmpty = true;
                Console.WriteLine(newNDC.ToString().Length);
                Console.WriteLine("Input fields are empty.");
                ClearAlertMessage();
            }
        }
    }
}