Feature: ToRange
  In order to support 
  As a composer
  I want to rescale controller change values

@extensions
Scenario: Values should scale properly at lower-bound of 0
  Given I have scaled the values to between 0 and 63
  When I provide the values "0, 4, 5, 63, 64, 65, 90, 126, 127"
  Then the result should be "0, 2, 31, 32, 45, 63"

@extensions
Scenario: Values should scale properly if the floor and ceiling are swapped
  Given I have scaled the values to between 64 and 1
  When I provide the values "0, 4, 5, 63, 64, 65, 90, 126, 127"
  Then the result should be "1, 3, 32, 33,  46, 64"

@extensions
Scenario: Values should not repeat
  Given I have scaled the values to between 30 and 35
  When I provide the values "0, 9, 34, 90, 1, 2, 30, 30"
  Then the result should be "30, 31, 34, 30, 31"

@extensions
Scenario: Values should be rooted at th lower bound
  Given I have scaled the values to between 64 and 127
  When I provide the values "0, 4, 5, 63, 64, 65, 90, 126, 127"
  Then the result should be "64, 66, 95, 96, 109, 127"

@extensions
Scenario: A scale of one value should only output one value
  Given I have scaled the values to between 34 and 34
  When I provide the values "0, 4, 5, 63, 64, 65, 90, 126, 127"
  Then the result should be 34

@extensions
Scenario: Two-value range
  Given I have scaled the values to between 0 and 1
  When I provide the values "0, 4, 5, 63, 64, 65, 90, 126, 127"
  Then the result should be "0, 1"

