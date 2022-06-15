/**
 * @param {number[]} nums
 * @param {number} lower
 * @param {number} upper
 * @return {string[]}
 */
const findMissingRanges = (nums, lower, upper) => {
	ranges = [];
	lastChecked = lower - 1;

	for (index = 0; index <= nums.length; ++index) {
		currentValue = index == nums.length ? upper + 1 : nums[index];

		if (currentValue - lastChecked > 1) {
			ranges.push(
				lastChecked + 1 != currentValue - 1
					? `${lastChecked + 1}->${currentValue - 1}`
					: `${currentValue - 1}`
			);
		}
		lastChecked = currentValue;
	}
	console.log(ranges);
};

// findMissingRanges([0, 1, 3, 50, 75], 0, 99);
