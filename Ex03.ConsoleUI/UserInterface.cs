﻿using System;
using System.Collections.Generic;
using Ex03.GarageLogic;

namespace Ex03.ConsoleUI
{
    ////    TO DO LIST:
    ////    1. make sure max values are not lower then current value entered
    ////    2. write ValueOutOfRangeException class for exception if entered to much
    ////       air pressure of fuel amount, throw from relevant classes and catch it here
    ////       this class should contain float MaxValue and float MinValue and inherits exception

    internal class UserInterface
    {
        private static string s_MenuChoiceInput;
        private static eVehiclesAvailable s_VehicleType;

        public static void RunGarage()
        {
            welcomeMessage();
            runUserInterface();
            exitMessage();
        }

        private static void exitMessage()
        {
            Console.WriteLine(Environment.NewLine + "Press enter to continue..");
            Console.ReadLine();
        }

        private static void welcomeMessage()
        {
            const string greetMessage = @"                ===========================================================
                =============== Welcome to our garage! ====================
                ===========================================================
                ";
            Console.WriteLine(greetMessage);
        }

        private static void showMenu()
        {
                string menuMessage = string.Format(
                    "---------------------------------------------------------------------------------" +
                    Environment.NewLine +
                    "Please choose your action:" +
                    Environment.NewLine +
                    Environment.NewLine +
                    "   1 - Register a new vehicle into the garage" +
                    Environment.NewLine +
                    "   2 - Show list of the vehicles registration ID's" +
                    Environment.NewLine +
                    "   3 - Update existing vehicle status" +
                    Environment.NewLine +
                    "   4 - Fill existing vehicle wheels air pressure to the max" +
                    Environment.NewLine +
                    "   5 - Fuel an existing vehicle (relevant to fuel powered vehicles only)" +
                    Environment.NewLine +
                    "   6 - Charge an existing vehicle (relevant to electric powered vehicles only)" +
                    Environment.NewLine +
                    "   7 - Show existing vehicle full details (using registration ID)" +
                    Environment.NewLine +
                    Environment.NewLine +
                    "Please enter 'q' to exit the system." +
                    Environment.NewLine +
                    "---------------------------------------------------------------------------------");
                Console.WriteLine(menuMessage);
        }

        private static void runUserInterface()
        {
            try
            {
                do
                {
                    showMenu();
                    s_MenuChoiceInput = Console.ReadLine();
                    InputValidations.checkValidMenuChoice(s_MenuChoiceInput);
                    char userMenuChoice = char.Parse(s_MenuChoiceInput ?? throw new FormatException());

                    switch (userMenuChoice)
                    {
                        case (char)eUserChoiceInMenu.RegisterNewVehicle:
                            {
                                registerNewVehicle();
                                break;
                            }

                        case (char)eUserChoiceInMenu.ShowAllExistingVehicles:
                            {
                                showAllExistingVehicles();
                                break;
                            }

                        case (char)eUserChoiceInMenu.UpdateVehicleStatus:
                            {
                                updateVehicleStatus();
                                break;
                            }

                        case (char)eUserChoiceInMenu.FillAllTires:
                            {
                                fillAllTires();
                                break;
                            }

                        case (char)eUserChoiceInMenu.FuelVehicle:
                            {
                                fuelVehicle();
                                break;
                            }

                        case (char)eUserChoiceInMenu.ChargeVehicle:
                            {
                                chargeVehicle();
                                break;
                            }

                        case (char)eUserChoiceInMenu.ShowVehicleFullDetails:
                            {
                                showVehicleFullDetails();
                                break;
                            }

                        case (char)eUserChoiceInMenu.Exit:
                            {
                                return;
                            }

                        default:
                            {
                                throw new FormatException();
                            }
                    }
                }
                while (true);
            }
            catch (FormatException i_FormatException)
            {
                string errorMessage = string.Format(
                    Environment.NewLine +
                    "-> Menu choice entered is invalid! Please choose again." +
                    Environment.NewLine +
                    "-> Error Details: " +
                    i_FormatException.Message +
                    Environment.NewLine);
                Console.WriteLine(errorMessage);
                runUserInterface();
            }
        }

        private static void registerNewVehicle()
        {
            try
            {
                string vehicleID = InputValidations.setVehicleId();

                if (!CreateAndSaveData.s_VehiclesInSystem.ContainsKey(vehicleID))
                {
                    string vehicleTypeMessage = string.Format(
                        "~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~" +
                        Environment.NewLine +
                        "Please choose the type of the vehicle you wish to add:" +
                        Environment.NewLine +
                        "   1 - Electric car" +
                        Environment.NewLine +
                        "   2 - Fueled car" +
                        Environment.NewLine +
                        "   3 - Electric motorcycle" +
                        Environment.NewLine +
                        "   4 - Fueled motorcycle" +
                        Environment.NewLine +
                        "   5 - Truck" +
                        Environment.NewLine +
                        "~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
                    Console.WriteLine(vehicleTypeMessage);
                    s_MenuChoiceInput = Console.ReadLine();
                    InputValidations.setChosenVehicle(s_MenuChoiceInput, ref s_VehicleType);
                    createChosenVehicle(vehicleID);
                }
                else
                {
                    Console.WriteLine("-> This vehicle is already in the system! Moving existing vehicle to repair now...");
                    CreateAndSaveData.UpdateVehicleStatus(eVehicleStatus.InRepair, vehicleID);
                }
            }
            catch (ArgumentException i_ArgumentException) //check if to change exception
            {
                string errorMessage = string.Format(
                    Environment.NewLine +
                    "-> Vehicle ID entered is invalid! Please make sure to type only number digits and try again." +
                    Environment.NewLine +
                    "-> Error Details: " +
                    i_ArgumentException.Message +
                    Environment.NewLine);
                Console.WriteLine(errorMessage);
                registerNewVehicle();
            }
            catch (FormatException i_FormatException)
            {
                string errorMessage = string.Format(
                    Environment.NewLine +
                    "-> Vehicle type choice entered is invalid! Please choose again." +
                    Environment.NewLine +
                    "-> Error Details: " +
                    i_FormatException.Message +
                    Environment.NewLine);
                Console.WriteLine(errorMessage);
                registerNewVehicle();
            }
        }

        private static void createChosenVehicle(string i_RegistrationId)
        {
            try
            {
                switch (s_VehicleType)
                {
                    case eVehiclesAvailable.FueledCar:
                        {
                            fueledCarCreation(
                                i_RegistrationId,
                                InputValidations.setOwnerName(),
                                InputValidations.setPhoneNumber(),
                                InputValidations.setWheelsManufacture(),
                                InputValidations.setWheelsCurrentAirPressure(),
                                InputValidations.setWheelsMaxAirPressure(),
                                InputValidations.setCarModel(),
                                InputValidations.setCarEnergyPercentage());
                            break;
                        }

                    case eVehiclesAvailable.ElectricCar:
                        {
                            electricCarCreation(
                                i_RegistrationId,
                                InputValidations.setOwnerName(),
                                InputValidations.setPhoneNumber(),
                                InputValidations.setWheelsManufacture(),
                                InputValidations.setWheelsCurrentAirPressure(),
                                InputValidations.setWheelsMaxAirPressure(),
                                InputValidations.setCarModel(),
                                InputValidations.setCarEnergyPercentage());
                            break;
                        }

                    case eVehiclesAvailable.ElectricMotorcycle:
                        {
                            electricMotorcycleCreation(
                                i_RegistrationId,
                                InputValidations.setOwnerName(),
                                InputValidations.setPhoneNumber(),
                                InputValidations.setWheelsManufacture(),
                                InputValidations.setWheelsCurrentAirPressure(),
                                InputValidations.setWheelsMaxAirPressure(),
                                InputValidations.setCarModel(),
                                InputValidations.setCarEnergyPercentage());
                            break;
                        }

                    case eVehiclesAvailable.FueledMotorcycle:
                        {
                            fueledMotorcycleCreation(
                                i_RegistrationId,
                                InputValidations.setOwnerName(),
                                InputValidations.setPhoneNumber(),
                                InputValidations.setWheelsManufacture(),
                                InputValidations.setWheelsCurrentAirPressure(),
                                InputValidations.setWheelsMaxAirPressure(),
                                InputValidations.setCarModel(),
                                InputValidations.setCarEnergyPercentage());
                            break;
                        }

                    case eVehiclesAvailable.Truck:
                        {
                            truckCreation(
                                i_RegistrationId,
                                InputValidations.setOwnerName(),
                                InputValidations.setPhoneNumber(),
                                InputValidations.setWheelsManufacture(),
                                InputValidations.setWheelsCurrentAirPressure(),
                                InputValidations.setWheelsMaxAirPressure(),
                                InputValidations.setCarModel(),
                                InputValidations.setCarEnergyPercentage());
                            break;
                        }

                    default:
                        {
                            throw new FormatException();
                        }
                }
            }
            catch (FormatException i_FormatException)
            {
                string errorMessage = string.Format(
                    Environment.NewLine +
                    "-> Value entered is invalid! Please type again correctly." +
                    Environment.NewLine +
                    "-> Error Details: " +
                    i_FormatException.Message +
                    Environment.NewLine);
                Console.WriteLine(errorMessage);
                createChosenVehicle(i_RegistrationId);
            }
        }

        private static void truckCreation(
            string i_RegistrationId,
            string i_OwnerName,
            string i_OwnerPhoneNumber,
            string i_WheelManufacture,
            float i_CurrentAirPressure,
            float i_MaxAirPressure,
            string i_CarModel,
            float i_EnergyPercentage)
        {
            CreateAndSaveData.CreateTruck(
                i_CarModel,
                i_RegistrationId,
                i_EnergyPercentage,
                i_WheelManufacture,
                i_CurrentAirPressure,
                i_MaxAirPressure,
                InputValidations.setIsTruckCooling(),
                InputValidations.setTruckMaxCapacity(),
                i_OwnerName,
                i_OwnerPhoneNumber,
                InputValidations.setFuelType(),
                InputValidations.setCurrentFuelStatus(),
                InputValidations.setMaxFuelCapacity());
        }

        private static void fueledMotorcycleCreation(
            string i_RegistrationId,
            string i_OwnerName,
            string i_OwnerPhoneNum,
            string i_WheelManufacture,
            float i_CurrentAirPressure,
            float i_MaxAirPressure,
            string i_CarModel,
            float i_EnergyPercentage)
        {
            CreateAndSaveData.CreateFueledMotorcycle(
                i_CarModel,
                i_RegistrationId,
                i_EnergyPercentage,
                i_WheelManufacture,
                i_CurrentAirPressure,
                i_MaxAirPressure,
                InputValidations.setLicenseType(),
                InputValidations.setEngineCapacity(),
                InputValidations.setFuelType(),
                InputValidations.setCurrentFuelStatus(),
                InputValidations.setMaxFuelCapacity(),
                i_OwnerName,
                i_OwnerPhoneNum);
        }

        private static void electricMotorcycleCreation(
            string i_RegistrationId,
            string i_OwnerName,
            string i_OwnerPhoneNum,
            string i_WheelManufacture,
            float i_CurrentAirPressure,
            float i_MaxAirPressure,
            string i_CarModel,
            float i_EnergyPercentage)
        {
            CreateAndSaveData.CreateElectricMotorcycle(
                i_CarModel,
                i_RegistrationId,
                i_EnergyPercentage,
                i_WheelManufacture,
                i_CurrentAirPressure,
                i_MaxAirPressure,
                InputValidations.setLicenseType(),
                InputValidations.setEngineCapacity(),
                InputValidations.setBatteryTimeLeft(),
                InputValidations.setBatteryMaxTime(),
                i_OwnerName,
                i_OwnerPhoneNum);
        }

        private static void electricCarCreation(
            string i_RegistrationId,
            string i_OwnerName,
            string i_OwnerPhoneNum,
            string i_WheelManufacture,
            float i_CurrentAirPressure,
            float i_MaxAirPressure,
            string i_CarModel,
            float i_EnergyPercentage)
        {
            CreateAndSaveData.CreateElectricCar(
                i_CarModel,
                i_RegistrationId,
                i_EnergyPercentage,
                i_WheelManufacture,
                i_CurrentAirPressure,
                i_MaxAirPressure,
                InputValidations.setCarColor(),
                InputValidations.setNumOfDoors(),
                InputValidations.setBatteryTimeLeft(),
                InputValidations.setBatteryMaxTime(),
                i_OwnerName,
                i_OwnerPhoneNum);
        }

        private static void fueledCarCreation(
            string i_RegistrationId,
            string i_OwnerName,
            string i_OwnerPhoneNum,
            string i_WheelManufacture,
            float i_CurrentAirPressure,
            float i_MaxAirPressure,
            string i_CarModel,
            float i_EnergyPercentage)
        {
            CreateAndSaveData.CreateFueledCar(
                i_CarModel,
                i_RegistrationId,
                i_EnergyPercentage,
                i_WheelManufacture,
                i_CurrentAirPressure,
                i_MaxAirPressure,
                InputValidations.setCarColor(),
                InputValidations.setNumOfDoors(),
                InputValidations.setFuelType(),
                InputValidations.setCurrentFuelStatus(),
                InputValidations.setMaxFuelCapacity(),
                i_OwnerName,
                i_OwnerPhoneNum);
        }

        private static void showAllExistingVehicles() // has exception, need to check its working good
        {
            Console.WriteLine("Would you like to show the list of ID's in the system filtered by their status? (yes/no)");
            string showBySortInput = Console.ReadLine();

            switch (showBySortInput?.ToLower())
            {
                case "yes":
                    {
                        eVehicleStatus filterBy = InputValidations.setVehicleStatus();

                        switch (filterBy)
                        {
                            case eVehicleStatus.InRepair:
                                {
                                    showAllVehicleInRepair();
                                    break;
                                }

                            case eVehicleStatus.Repaired:
                                {
                                    showAllVehicleRepaired();
                                    break;
                                }

                            case eVehicleStatus.Paid:
                                {
                                    showAllVehiclePaid();
                                    break;
                                }

                            default:
                                {
                                    throw new FormatException();
                                }
                        }

                        break;
                    }

                case "no":
                    {
                        Console.WriteLine(Environment.NewLine + "All registered cars id's:" + Environment.NewLine);
                        foreach (string carId in CreateAndSaveData.s_AllVehiclesIds)
                        {
                            Console.WriteLine(carId);
                        }

                        Console.Write(Environment.NewLine);
                        break;
                    }

                default:
                    {
                        throw new FormatException();
                    }
            }
        }

        private static void showAllVehicleInRepair()
        {
            if (CreateAndSaveData.s_AllVehiclesIdsInRepair.Count != 0)
            {
                Console.WriteLine("List of the vehicles that are in repair:" + Environment.NewLine);
                foreach (string carId in CreateAndSaveData.s_AllVehiclesIdsInRepair)
                {
                    Console.WriteLine(carId);
                }
            }
            else
            {
                Console.WriteLine("There are no vehicles currently in \"In Repair\" status.");
            }
        }

        private static void showAllVehicleRepaired()
        {
            if (CreateAndSaveData.s_AllVehiclesIdsRepaired.Count != 0)
            {
                Console.WriteLine("List of the vehicles that were repaired:" + Environment.NewLine);
                foreach (string carId in CreateAndSaveData.s_AllVehiclesIdsRepaired)
                {
                    Console.WriteLine(carId);
                }
            }
            else
            {
                Console.WriteLine("There are no vehicles in Repaired status.");
            }
        }

        private static void showAllVehiclePaid()
        {
            if (CreateAndSaveData.s_AllVehiclesIdsPaid.Count != 0)
            {
                Console.WriteLine("List of the vehicles that were paid:" + Environment.NewLine);
                foreach (string carId in CreateAndSaveData.s_AllVehiclesIdsPaid)
                {
                    Console.WriteLine(carId);
                }
            }
            else
            {
                Console.WriteLine("There are no vehicles in paid status.");
            }
        }

        private static void updateVehicleStatus()
        {
            CreateAndSaveData.UpdateVehicleStatus(InputValidations.setVehicleStatus(), InputValidations.setVehicleId());
            Console.WriteLine("Vehicle status updated!" + Environment.NewLine);
        }

        private static void fillAllTires()
        {
            string carId = InputValidations.setVehicleId();
            CreateAndSaveData.s_VehiclesInSystem[carId].FillAirInTiresToTheMax();
            Console.WriteLine("All vehicle tires were filled to max capacity of air pressure!" + Environment.NewLine);
        }

        private static void fuelVehicle()
        {
            Console.WriteLine("Please enter the amount you wish to fill (Litters for fueled vehicle):");
            CreateAndSaveData.verifyVehicleTypeAndFuelVehicle(InputValidations.setAmountToFuelOrChargeVehicle(), InputValidations.setFuelType(), InputValidations.setVehicleId());
            Console.WriteLine("Vehicle has been fueled!" + Environment.NewLine);
        }

        private static void chargeVehicle()
        {
            Console.WriteLine("Please enter the amount you wish to fill (minutes for electric vehicle):");
            CreateAndSaveData.verifyVehicleTypeAndChargeVehicle(InputValidations.setAmountToFuelOrChargeVehicle(), InputValidations.setVehicleId());
            Console.WriteLine("Vehicle has been charged!" + Environment.NewLine);
        }

        private static void showVehicleFullDetails()
        {
            try
            {
                string idToShow = InputValidations.setVehicleId();
                Console.WriteLine(CreateAndSaveData.s_VehiclesInSystem[idToShow]);
            }
            catch (KeyNotFoundException i_KeyNotFoundException)
            {
                string errorMessage = string.Format(
                    Environment.NewLine +
                    "-> This ID is not registered in our garage! Please type existing ID." +
                    Environment.NewLine +
                    "-> Error Details: " +
                    i_KeyNotFoundException.Message +
                    Environment.NewLine);
                Console.WriteLine(errorMessage);
                showVehicleFullDetails();
            }
            catch (ArgumentException i_ArgumentException)
            {
                string errorMessage = string.Format(
                    Environment.NewLine +
                    "-> Vehicle ID entered is invalid! Please make sure to type only number digits and try again." +
                    Environment.NewLine +
                    "-> Error Details: " +
                    i_ArgumentException.Message +
                    Environment.NewLine);
                Console.WriteLine(errorMessage);
                registerNewVehicle();
            }
        }
    }
}
