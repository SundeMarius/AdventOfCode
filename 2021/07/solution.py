import sys
import statistics as st

def fuel_consumption_1(target, positions):
    return sum(abs(x - target) for x in positions)

def fuel_consumption_2(target, positions):
    deviations = [abs(x-target) for x in positions]
    return sum(d*(d+1)/2 for d in deviations)

positions = [int(x) for x in open(sys.argv[1]).read().strip('\n').split(',')]

# Part 1: the median m of a discrete set of numbers minimizes absolute total deviation: sum(|x_i - m|)
target_pos = st.median(positions)
fuel = fuel_consumption_1(target_pos, positions)
print("Part I) Fuel spent to reach optimal position (%i): %i" % (target_pos, fuel))

# Part 2: scan over small region close to the mean position
x_mean = int(st.mean(positions))
results = []
for x in range(x_mean - 5, x_mean + 5):
    result = (x, fuel_consumption_2(x, positions))
    results.append(result)
print("Part II) Fuel spent to reach optimal position (%i): %i" % min(results, key=lambda fuel: fuel[1]))
