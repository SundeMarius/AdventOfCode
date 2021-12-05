import sys

def check_point(point: tuple, points: dict):
    if point in points:
        points[point] += 1
        return
    points[point] = 1

def get_input(part: int):
    lines = open(sys.argv[1]).read().splitlines()
    start_points = []
    end_points = []
    for line in lines:
        line = line.split(" -> ")
        start = tuple(map(int, line[0].split(',')))
        end = tuple(map(int, line[1].split(',')))
        condition1 = start[0] == end[0] or start[1] == end[1]
        condition2 = abs(end[1] - start[1]) == abs(end[0] - start[0])
        if condition1: 
            start_points.append(start)
            end_points.append(end)
        if part == 2:
            if condition2:
                start_points.append(start)
                end_points.append(end)
    return start_points, end_points

def part1():
    start_points, end_points = get_input(1)
    points = {}
    for start, end in zip(start_points, end_points):
        x0 = start[0]
        y0 = start[1]
        x1 = end[0]
        y1 = end[1]
        if x0 == x1:
            direction = (1 if y1 > y0 else -1)
            for y in range(y0, y1 + direction, direction):
                point = (x0, y)
                check_point(point, points)
        else:
            direction = (1 if x1 > x0 else -1)
            for x in range(x0, x1 + direction, direction):
                point = (x, y0)
                check_point(point, points)
    print("PART I) Number of points with at least two lines overlap: ", sum(1 for occ in points.values() if occ >= 2))

def part2():
    start_points, end_points = get_input(2)
    points: dict[tuple, int] = {}
    for start, end in zip(start_points, end_points):
        x0 = start[0]
        y0 = start[1]
        x1 = end[0]
        y1 = end[1]
        if x0 == x1:
            direction = (1 if y1 > y0 else -1)
            for y in range(y0, y1 + direction, direction):
                point = (x0, y)
                check_point(point, points)
        elif y0 == y1:
            direction = (1 if x1 > x0 else -1)
            for x in range(x0, x1 + direction, direction):
                point = (x, y1)
                check_point(point, points)
        else:
            dir_x = (1 if x1 > x0 else -1)
            dir_y = (1 if y1 > y0 else -1)
            i = 0
            for x in range(x0, x1 + dir_x, dir_x):
                y = y0 + dir_y*i
                point = (x, y)
                check_point(point, points)
                i += 1
    print("PART II) Number of points with at least two lines overlap: ", sum(1 for occ in points.values() if occ >= 2))

part1()
part2()
