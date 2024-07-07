The solution requires .net 8 SDK. There are no other dependencies. Just hit F5 to run the solution.

A few comments about the implementation:
- I avoided using inheritance (e.g. dedicated BaseTariff class inherited from abstract class) for tariff and cost 
calculation mostly to avoid being blocked when we need to implement new calculation logic for a new tariff type. 
Potentially, we might have a two-year tariff or half-year tariff. There might be discount logic for some tariffs as well.
We might have progressive discount logic e.g. if the contract is prolonged for the second year then a customer gets a 5% discount. 
That's why I decided to use calculators instead of incapsulating calculation logic into classes dedicated to each tariff type
(e.g. BasicTariff class with Calculate method). Probably I'm biased here because of the "composition over inheritance" paradigm. 
With this approach, I see a problem with validation. There might be an instance of an invalid tariff while with dedicated 
classes validation logic can be encapsulated in classes so we are not able to instantiate invalid tariffs.
- Although, there were no specific requirements for the Tariff Providers,
I found the problem of extending the list of providers quite interesting.
So I decided to spend some time on that part of the solution just out of curiosity.
The result is not perfect but to make it better I need more time. The logic for every provider can be extracted into the dedicated projects.
Probably I spent more time on this part than I should have.
- Some infrastructure things like logging or global exception handler were not implemented because I didn't have enough time for that. I marked it with TODO like some other places in the code.
- There are a few unit tests for calculation logic. I skipped unit tests for services just because there is no complex logic and unit tests for such logic don't add any value to the take-home coding task.