#!/usr/bin/python3
import sys

def print_solution(position):
    print("Total forward: %i" % position[0])
    print("Total depth: %i" % position[1])
    print("Multiplied together: %i" % (position[0]*position[1]))

with open(sys.argv[1]) as f:

    lines = f.readlines()

    position = [0,0]
    for line in lines:
        direction, displacement = line.split(' ')
        displacement = int(displacement)
        if direction == "forward":
            position[0] += displacement
        elif direction == "up":
            position[1] -= displacement
        elif direction == "down":
            position[1] += displacement
    print("Part I:")
    print_solution(position)
    
    aim = 0
    position = [0,0]
    for line in lines:
        direction, displacement = line.split(' ')
        displacement = int(displacement)
        if direction == "forward":
            position[0] += displacement
            position[1] += aim*displacement
        elif direction == "up":
            aim -= displacement
        elif direction == "down":
            aim += displacement
    print("\nPart II:")
    print_solution(position)
