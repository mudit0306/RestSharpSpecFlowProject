Feature: Specflow POST Test Feature

Scenario Outline: POST service test
	Given I have URI <URI> with email <email> and password <password>
	When I hit the uri 
	Then I should receive Token and Required <StatusCode>

	Examples: :
		| URI                | email                  | password | StatusCode | 
		| https://reqres.in/ | janet.weaver@reqres.in | Janet123 | 200        |  
		| https://reqres.in/ | mudit.gaur@test.in     | Mudit123 | 400        |    