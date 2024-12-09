using UnityEngine;
using System;

public class DeveloperBuildGUI : MonoBehaviour
{
    private string buildInfo;

    private void Awake()
    {
        if (Debug.isDebugBuild)
        {
            string productName = "Espacio Publico";

            // Definir la zona horaria de Colombia
            TimeZoneInfo colombiaTimeZone;
            try
            {
                colombiaTimeZone = TimeZoneInfo.FindSystemTimeZoneById("America/Bogota");
            }
            catch (TimeZoneNotFoundException)
            {
                Debug.LogWarning("Time zone 'America/Bogota' no está disponible. Usando UTC.");
                colombiaTimeZone = TimeZoneInfo.Utc;
            }

            DateTime colombiaDateTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, colombiaTimeZone);

            string currentDate = colombiaDateTime.ToString("ddMMyyyy");
            string currentHour = colombiaDateTime.ToString("HHmm");

            buildInfo = $"{productName}_{currentDate}_{currentHour}";
            Debug.Log(buildInfo);
        }
    }

    private void OnGUI()
    {
        if (Debug.isDebugBuild)
        {
            // Ajustar el ancho y alto de la pantalla
            float screenWidth = Screen.width;
            float screenHeight = Screen.height;

            // Posicionar el texto en la esquina inferior izquierda
            GUI.Label(new Rect(10, screenHeight - 30, 500, 20), buildInfo);
        }
    }
}
