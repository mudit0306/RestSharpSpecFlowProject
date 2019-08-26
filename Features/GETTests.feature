Feature: Specflow GET Test feature file

Scenario Outline: Get service test
	Given I have the <URI> and <Parameters>
	When I Execute Request
	Then I should get <StatusCode>

	Examples: :
		| URI                | Parameters | StatusCode |
		| https://reqres.in/ | users      | 200        |
		| https://reqres.in/ |            | 404        |

		