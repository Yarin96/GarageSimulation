﻿using System;

namespace Ex03.GarageLogic
{
    using System.Collections.Generic;

    public class CreateAndSaveData
    {
        private static int s_VehicleNumOfWheels;
        public static Dictionary<string, object> m_VehiclesInSystem = new Dictionary<string, object>();

        private static Vehicle createVehicle(
            eVehicleType i_VehicleType,
            string i_ModelName,
            string i_RegistrationId,
            float i_EnergyPercentageLeft,
            string i_WheelModuleName,
            float i_CurrentAirPressure,
            float i_MaxAirPressure)
        {
            switch(i_VehicleType)
            {
                case eVehicleType.Motorcycle:
                    s_VehicleNumOfWheels = 2;
                    break;
                case eVehicleType.Car:
                    s_VehicleNumOfWheels = 4;
                    break;
                case eVehicleType.Truck:
                    s_VehicleNumOfWheels = 16;
                    break;
                default:
                    s_VehicleNumOfWheels = 4;
                    break;
            }

            Wheel[] carWheels = new Wheel[s_VehicleNumOfWheels];

            for (int i = 0; i < carWheels.Length; i++)
            {
                carWheels[i] = new Wheel(i_WheelModuleName, i_CurrentAirPressure, i_MaxAirPressure);
            }

            return new Vehicle(i_ModelName, i_RegistrationId, i_EnergyPercentageLeft, carWheels);
        }

        private static Car createCar(
            string i_ModelName,
            string i_RegistrationId,
            float i_EnergyPercentageLeft,
            string i_WheelModuleName,
            float i_CurrentAirPressure,
            float i_MaxAirPressure,
            eCarColor i_CarColor,
            eNumOfDoors i_NumOfDoors)
        {
            const eVehicleType k_Type = eVehicleType.Car;
            Vehicle vehicle = createVehicle(k_Type, i_ModelName, i_RegistrationId, i_EnergyPercentageLeft, i_WheelModuleName, i_CurrentAirPressure, i_MaxAirPressure);
            return new Car(vehicle, i_CarColor, i_NumOfDoors);
        }

        private static Motorcycle createMotorcycle(
            string i_ModelName,
            string i_RegistrationId,
            float i_EnergyPercentageLeft,
            string i_WheelModuleName,
            float i_CurrentAirPressure,
            float i_MaxAirPressure,
            eLicenseType i_LicenseType,
            int i_EngineCapacity)
        {
            const eVehicleType k_Type = eVehicleType.Motorcycle;
            Vehicle vehicle = createVehicle(k_Type, i_ModelName, i_RegistrationId, i_EnergyPercentageLeft, i_WheelModuleName, i_CurrentAirPressure, i_MaxAirPressure);
            return new Motorcycle(vehicle, i_LicenseType, i_EngineCapacity);
        }

        public static void CreateElectricCar(
            string i_ModelName,
            string i_RegistrationId,
            float i_EnergyPercentageLeft,
            string i_WheelModuleName,
            float i_CurrentAirPressure,
            float i_MaxAirPressure,
            eCarColor i_CarColor,
            eNumOfDoors i_NumOfDoors,
            float i_CurrentBatteryTimeLeft,
            float i_MaxBatteryCapacityTime)
        {
            Car car = createCar(i_ModelName, i_RegistrationId, i_EnergyPercentageLeft, i_WheelModuleName, i_CurrentAirPressure, i_MaxAirPressure, i_CarColor, i_NumOfDoors);
            m_VehiclesInSystem.Add(i_RegistrationId, new ElectricCar(car, i_CurrentBatteryTimeLeft, i_MaxBatteryCapacityTime));
        }

        public static void CreateFueledCar(
            string i_ModelName,
            string i_RegistrationId,
            float i_EnergyPercentageLeft,
            string i_WheelModuleName,
            float i_CurrentAirPressure,
            float i_MaxAirPressure,
            eCarColor i_CarColor,
            eNumOfDoors i_NumOfDoors,
            eFuelType i_FuelType,
            float i_CurrentFuelStatus,
            float i_MaxFuelCapacity)
        {
            Car car = createCar(i_ModelName, i_RegistrationId, i_EnergyPercentageLeft, i_WheelModuleName, i_CurrentAirPressure, i_MaxAirPressure, i_CarColor, i_NumOfDoors);
            m_VehiclesInSystem.Add(i_RegistrationId, new FueledCar(car, i_FuelType, i_CurrentFuelStatus, i_MaxFuelCapacity));
        }

        public static void CreateElectricMotorcycle(
            string i_ModelName,
            string i_RegistrationId,
            float i_EnergyPercentageLeft,
            string i_WheelModuleName,
            float i_CurrentAirPressure,
            float i_MaxAirPressure,
            eLicenseType i_LicenseType,
            int i_EngineCapacity,
            float i_CurrentBatteryTimeLeft,
            float i_MaxBatteryCapacityTime)
        {
            Motorcycle motorcycle = createMotorcycle(
                i_ModelName,
                i_RegistrationId,
                i_EnergyPercentageLeft,
                i_WheelModuleName,
                i_CurrentAirPressure,
                i_MaxAirPressure,
                i_LicenseType,
                i_EngineCapacity);
            m_VehiclesInSystem.Add(i_RegistrationId, new ElectricMotorcycle(motorcycle, i_CurrentBatteryTimeLeft, i_MaxBatteryCapacityTime));
        }

        public static void CreateFueledMotorcycle(
            string i_ModelName,
            string i_RegistrationId,
            float i_EnergyPercentageLeft,
            string i_WheelModuleName,
            float i_CurrentAirPressure,
            float i_MaxAirPressure,
            eLicenseType i_LicenseType,
            int i_EngineCapacity,
            eFuelType i_FuelType,
            float i_CurrentFuelStatus,
            float i_MaxFuelCapacity)
        {
            Motorcycle motorcycle = createMotorcycle(
                i_ModelName,
                i_RegistrationId,
                i_EnergyPercentageLeft,
                i_WheelModuleName,
                i_CurrentAirPressure,
                i_MaxAirPressure,
                i_LicenseType,
                i_EngineCapacity);
            m_VehiclesInSystem.Add(i_RegistrationId, new FueledMotorcycle(motorcycle, i_FuelType, i_CurrentFuelStatus, i_MaxFuelCapacity));
        }

        public static void CreateTruck(
            string i_ModelName,
            string i_RegistrationId,
            float i_EnergyPercentageLeft,
            string i_WheelModuleName,
            float i_CurrentAirPressure,
            float i_MaxAirPressure,
            bool i_TransportWithCooling,
            float i_MaxCargoWeight)
        {
            const eVehicleType k_Type = eVehicleType.Truck;
            Vehicle vehicle = createVehicle(k_Type, i_ModelName, i_RegistrationId, i_EnergyPercentageLeft, i_WheelModuleName, i_CurrentAirPressure, i_MaxAirPressure);
            m_VehiclesInSystem.Add(i_RegistrationId, new Truck(vehicle, i_TransportWithCooling, i_MaxCargoWeight));
        }
    }
}
