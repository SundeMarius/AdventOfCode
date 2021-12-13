class Octopus:
    def __init__(self, position, energy):
        self.position = position
        self.energy = energy
    def step(self):
        self.energy += 1
    def flash(self):
        self.energy = 0
    def is_fully_charged(self):
        return self.energy > 9
    def neighbors(self, nrows, ncols):
        y, x = self.position
        candidates = [(y+1,x+1), (y,x+1), (y-1,x+1), (y-1,x),
                      (y-1,x-1), (y,x-1), (y+1,x-1), (y+1,x)]
        neighbors = []
        for yc, xc in candidates:
            if 0 <= yc < nrows and 0 <= xc < ncols:
                neighbors.append((yc, xc))
        return neighbors

lines = open('input.txt').read().splitlines()
matrix = [[Octopus((y,x), int(e)) for x, e in enumerate(line)] for y, line in enumerate(lines)]
nrows, ncols = 10, 10

def octopuses_to_flash(octopus: Octopus, octopuses: set[Octopus] = {}):
    neighbors = octopus.neighbors(nrows, ncols)
    for ny, nx in neighbors:
        neighbor = matrix[ny][nx]
        neighbor.step()
    for ny, nx in neighbors:
        neighbor = matrix[ny][nx]
        if neighbor.is_fully_charged() and neighbor not in octopuses:
            octopuses.add(neighbor)
            octopuses_to_flash(neighbor, octopuses)

# Part 1 and part 2:
def solution():
    answer1 = 0
    answer2 = 0
    step = 0
    while True:
        for y in range(nrows):
            for x in range(ncols):
                octopus = matrix[y][x]
                octopus.step()
        flash_octopuses: set[Octopus] = set()
        for y in range(nrows):
            for x in range(ncols):
                octopus = matrix[y][x]
                if octopus.is_fully_charged() and octopus not in flash_octopuses:
                    flash_octopuses.add(octopus)
                    octopuses_to_flash(octopus, flash_octopuses)
        for octopus in flash_octopuses:
            octopus.flash()
        # Part 1
        if step < 100:
            answer1 += len(flash_octopuses)
        # Part 2
        if len(flash_octopuses) == nrows*ncols:
            answer2 = step+1
            return answer1, answer2
        step += 1
answer1, answer2 = solution()
print("Part 1: %i, Part 2: %i" %(answer1, answer2))