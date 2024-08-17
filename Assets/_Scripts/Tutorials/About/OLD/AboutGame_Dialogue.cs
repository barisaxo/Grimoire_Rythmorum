// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using Dialog;
// using System;

// public class AboutGame_Dialogue : Dialogue
// {
//     public override Dialogue Initiate()
//     {
//         FirstLine = GetLines();
//         return this;
//     }

//     private Line GetLines()
//     {
//         var about = _about;

//         Line[] lines = new Line[about.Length];
//         for (int i = 0; i < lines.Length; i++)
//         {
//             lines[i] = new(about[i]);
//         }

//         lines[^1].SetResponses(new Response[2] { new Response("Previous", lines[^2]), Exit });

//         for (int i = 1; i < lines.Length - 1; i++)
//         {
//             lines[i].SetResponses(Replies(lines[i + 1], lines[i - 1]));
//         }

//         lines[0].SetResponses(new Response[2] { new Response("Next", lines[1]), Exit });

//         return lines[0];
//     }

//     Response _exit;
//     Response Exit => _exit ??= new Response("Exit", new TutorialMenu_Dialogue());
//     Response[] Replies(Line nextLine, Line prevLine) => new Response[3]{
//             new Response("Next", nextLine),
//             new Response("Previous", prevLine),
//             Exit,
//     };

//     string[] _about => new string[3]{
//         "The goal of ",
//         " ",
//         " ",



//     };
// }
