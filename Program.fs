﻿// Лабораторная работа 1 – Основы функционального программирования
// Выполнить задания с использованием функций высших порядков:
// Для всех чисел от 1 до 100 найти сумму чисел, кратных 3 и 5
let sum = List.fold (fun acc next -> if (next % 5 = 0) && (next % 3 = 0) then acc + next else acc) 0 [1..100]
printf "%d" sum