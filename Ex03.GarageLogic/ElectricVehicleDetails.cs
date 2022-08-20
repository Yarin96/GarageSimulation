﻿using System;

namespace Ex03.GarageLogic
{
    public class ElectricVehicleDetails
    {
        private readonly float r_MaxBatteryCapacityTime;
        private float m_CurrentBatteryTimeLeft;

        public ElectricVehicleDetails(float i_MaxBatteryCapacityTime, float i_CurrentBatteryTimeLeft)
        {
            this.r_MaxBatteryCapacityTime = i_MaxBatteryCapacityTime;
            this.m_CurrentBatteryTimeLeft = i_CurrentBatteryTimeLeft;

        }

        public bool ChargeVehicle(float i_HoursToAddBatteryTime) // add exception
        {
            bool canChargeVehicle = true;

            if (m_CurrentBatteryTimeLeft + i_HoursToAddBatteryTime > r_MaxBatteryCapacityTime)
            {
                canChargeVehicle = false;
            }
            else
            {
                m_CurrentBatteryTimeLeft += i_HoursToAddBatteryTime;
            }

            return canChargeVehicle;
        }

        public override string ToString()
        { 
            return string.Format(Environment.NewLine + "Current battery time left(in hours): {0}" + Environment.NewLine + "Max battery capacity time(in hours): {1}", m_CurrentBatteryTimeLeft, r_MaxBatteryCapacityTime);
        }
    }
}