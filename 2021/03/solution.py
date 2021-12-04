#!/usr/bin/python3
import sys
import statistics as st

def invert_bits(n, num_bits):
    # Example: ~1001 = 1111 - 1001 = 0110
    return (1 << num_bits) - 1 - n

with open(sys.argv[1]) as f:

    nums = f.read().splitlines()
    nums_cpy = nums.copy()
    num_bits = len(nums[0])
    
    # Part I
    gamma_rate = ""
    for i in range(num_bits):
        column = [b[i] for b in nums]
        gamma_rate += st.mode(column)
    gamma_rate = int(gamma_rate, 2)
    epsilon_rate = invert_bits(gamma_rate, num_bits)
    power_consumption = gamma_rate*epsilon_rate
    print("Gamma rate:   %s = %i" % (bin(gamma_rate), gamma_rate))
    print("Epsilon rate: %s = %i" % (bin(epsilon_rate), epsilon_rate))
    print("------------")
    print("Power consumption: %i\n\n" % power_consumption)

    # Part II
    for i in range(num_bits):
        column = [b[i] for b in nums]
        keep_bit = '1'
        if column.count('0') > column.count('1'):
            keep_bit = '0'
        # Keep only numbers with the "keep_bit" at pos 'i'
        nums = [num for num in nums if num[i] == keep_bit]
        # Stop when one number is left
        if len(nums) == 1:
            break

    for i in range(num_bits):
        column = [b[i] for b in nums_cpy]
        keep_bit = '0'
        if column.count('0') > column.count('1'):
            keep_bit = '1'
        # Keep only numbers with the "keep_bit" at pos 'i'
        nums_cpy = [num for num in nums_cpy if num[i] == keep_bit]
        # Stop when one number is left
        if len(nums_cpy) == 1:
            break

    oxgen_rating = int(nums[0], 2)
    co2_rating = int(nums_cpy[0], 2)
    life_support = oxgen_rating * co2_rating
    print("Oxygen generator rating: %s = %i" % (nums[0], oxgen_rating))
    print("CO2 scrubber rating:     %s = %i" % (nums_cpy[0], co2_rating))
    print("------------")
    print("Life support rating: %i" % life_support)
