import sys
import numpy as np

# Initial state (keeping count of fishes with remaining time 0, 1, 2, ..., 8 with a 9D-vec)
lines = [int(fish) for fish in open(sys.argv[1]).read().strip('\n').split(',')]
s0 = np.array([lines.count(f) for f in range(9)])

""" Transition matrix 
    1) Reduce number of days by 1 for each fish, add number of 0-fishes to number of 6-fishes.
    2) Each 0-fish produces an 8-fish.
"""
M = np.matrix("0 1 0 0 0 0 0 0 0;"
              "0 0 1 0 0 0 0 0 0;"
              "0 0 0 1 0 0 0 0 0;"
              "0 0 0 0 1 0 0 0 0;"
              "0 0 0 0 0 1 0 0 0;"
              "0 0 0 0 0 0 1 0 0;"
              "1 0 0 0 0 0 0 1 0;"
              "0 0 0 0 0 0 0 0 1;"
              "1 0 0 0 0 0 0 0 0")
# Solution is simply the sum of state vector components (#fishes)
print("Number of fishes after 80 days: ", int(np.sum(M**80@s0)))
print("Number of fishes after 256 days: ", int(np.sum(M**256@s0)))
