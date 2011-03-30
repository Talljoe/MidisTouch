Feature: Clamp
  In order to prevent out-of-range values
  As a composer
  I want to be clamp ontroller change values to a specific range

@extensions
Scenario: Values should be clamped
  Given I have clamped the values between 5 and 103
  When I provide the values "5, 63, 64, 65, 90, 126"
  Then the result should be "5, 63, 64, 65, 90, 103"

@extensions
Scenario: Values should not repeat
  Given I have clamped the values between 10 and 20
  When I provide the values "5, 63, 3, 19, 90"
  Then the result should be "10, 20, 10, 19, 20"

@extensions
Scenario: Values should be clamped normally if floor and ceiling are reversed
  Given I have clamped the values between 20 and 10
  When I provide the values "5, 63, 3, 19, 90"
  Then the result should be "10, 20, 10, 19, 20"
