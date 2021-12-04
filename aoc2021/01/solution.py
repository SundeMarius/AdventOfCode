#!/usr/bin/python3
import sys

with open(sys.argv[1]) as f:

	numbers = f.readlines()
	n = len(numbers)

	counter1 = 0
	for i in range(n-1):
		counter1 += (int(numbers[i+1]) > int(numbers[i]))
	print("Task1) Number of increases: %i" % counter1)

	counter2 = 0
	for i in range(n-3):
		counter2 += (int(numbers[i+3]) > int(numbers[i]))
	print("Task2) Number of increases: %i" % counter2)
