# FSL.CacheProvider

**Cache Provider in one line of code**

In this repository I will share my own Cache Provider implementation to use everywhere calling in one line of code.

The cache provider follows SOLID principles.

![enter image description here](https://fabiosilvalima.net/wp-content/uploads/2017/01/fabiosilvalima-cache-provider-em-uma-linha-de-codigo.jpg)

> **LIVE DEMO:**
> 
> http://codefinal.com/FSL.CacheProvider

> **FULL ARTICLE:**
>
> English: https://fabiosilvalima.net/en/cache-provider-one-line-code/
>
> Português: https://fabiosilvalima.net/cache-provider-em-uma-linha-de-codigo/

---

What is in the source code?
---

#### <i class="icon-file"></i> FSL.CacheProvider

- MVC application to demonstrate caching.
- Cache interfaces.
- Cache implementations.
- Cache provider.

> **Remarks:**

> - I created a MVC application application in Visual Studio 2015. 
> - Use the same virtual directory from this article

---

What is the goal?
---

- Demonstrate my Cache Provider using parallelism calling all methods asynchronously.

**Assumptions:**

- You need to create a virtual directory "FSL.CacheProvider" in your IIS for that application.


Code and usage:
---

```csharp
public async Task<ActionResult> Index(string id = "0")
{
      _dateTime = DateTime.Now;
 
      var customer = _cacheProvider.CacheAsync(() => GetCustomer(id), id);
      var genders = _cacheProvider.CacheAsync(() => GetGenders());
      var userName = _cacheProvider.CacheAsync(UseCacheOrNot(), () => GetUserName(_loggedUser), _loggedUser);
 
      await Task.WhenAll(customer, genders, userName);
 
      var viewModel = new HomePageViewModel()
      {
           Customer = customer.Result,
           Genders = genders.Result,
           UserName = userName.Result,
           Date = _dateTime
       };
 
       return View(viewModel);
}
```


References:
---

- More at my blog click here [fabiosilvalima.net][1]
- Live [demo][4]

Licence:
---

- Licence MIT


---

![Programação no Mundo Real Design Patterns Vol. 1](https://www.fabiosilvalima.net/wp-content/uploads/2017/02/fabiosilvalima-ebook-design-patterns-INSTAGRAM-2.png)

  [1]: http://fabiosilvalima.net
