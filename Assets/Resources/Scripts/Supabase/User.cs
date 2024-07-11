using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class User : MonoBehaviour
{
    public string Username { get; set; }
    public string Email { get; set; }
    public static string CurrentEmail { get; set; }
    public static string CurrentUsername { get; set; }

    public static User CurrentUser { get; private set; }
}