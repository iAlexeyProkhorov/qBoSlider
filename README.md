# qBoSlider
Hi everyone👋!

Today I want to present you my small slider project! 
I had good expirience of work with [nopCommerce](https://www.nopcommerce.com/en/) CMS and sometimes I get multilanguage slider tasks. I work few years and received slider task often. nopCommerce plugins marketplace has no free plugin for multilanguage slides. That's why I start work on my own multilanguage slider for nopCommerce.
Slider user interface features implemented via [JSSOR slider](https://github.com/jssor).

Today qBoSlider is absolutely free nopCommerce slider with next features:

* Unlimited widget zones quantity support. You can create slider for each widget zone in your store.
* Unlimited slides quantity.
* Multilanguage slide content.
* HTML content supported.
* Fast content loading via cache.
* Multilanguage slide images. You can select slide background image for each store language.
* Simple configuration and customization.
* Responsive design support.
* ACL support. You can select which customer roles will see the slide.
* Multistore support. You can display slides only in needed stores.
* Slide activity time support. You can set date from which slide will be visible and when it become invisible again.

### Installation:
* Uninstall and remove previous plugin version if you have it. Previous plugin versions aren't compatible with multiwidget zone version;
* Download plugin archive.
* Go to admin area > configuration > local plugins.
* Upload the plugin archive using the "Upload plugin or theme" plugin.
Scroll down through the list of plugins to find the newly installed plugin. And click on the "Install" button to install the plugin.
* Find plugin in list or at admin area > configuration > widgets and mark plugin as active.
Congratulations! You're installed qBoSlider. By default slider displays three slides on site homepage.

Hi everyone. Here is the simple instruction how to use updated qBoSlider. 
As you can see we change slider configuration page. At now slider configuration page contains only **Switch on static cache** property, but don't panic, other slider configurations you can find at nopCommerce admin panel menu 😉. Just open admin side -> menu -> Plugins -> qBoSlider and you can configure all that you want.

![](https://1drv.ms/u/s!ArjbDezvZ070grM2hwDV5nbCc-A5hg)

## Menu
The plugin menu contains three points:
* **[Widget zones](https://github.com/iAlexeyProkhorov/qBoSlider/wiki/Widget-zone)**. Here you can create new or update already existing sliders in your store. Each slider is linked to one special [widget zone](https://github.com/iAlexeyProkhorov/qBoSlider/wiki/Widget-zone). By default plugin has one [slider](https://github.com/iAlexeyProkhorov/qBoSlider/wiki/Widget-zone) on home page;
* **[Slides](https://github.com/iAlexeyProkhorov/qBoSlider/wiki/Slide)**. Here you can create or update slides for your store. Each slide can be linked to any sliders any times. 
* **Configuration**. Here you can configure plugin.

## Slide
First of all we need to create some slides for our store, before publish them in [slider](https://github.com/iAlexeyProkhorov/qBoSlider/wiki/Widget-zone). We can do this by next way:
* Open: Admin -> Admin menu -> Plugins -> qBoSlider -> Slides.
* Push '_Add new slide_' button;
* Upload background picture for your slide and save slide;
* * You can also add slide HTML content in '_Description_' field. It's not required option;
* Congratulations! You already create your first [slide](https://github.com/iAlexeyProkhorov/qBoSlider/wiki/Slide)!🎉😉

Previously we just create slide and after clearing cache we can see it on home page. In multi-widget zone slider we can select which widget zone will displays our slide. And we are we going to next step 😊.

## Widget zone
As we know, the widget zone represents a [slider](https://github.com/iAlexeyProkhorov/qBoSlider/wiki/Widget-zone). Each [slider has some common properties and slide collection](https://github.com/iAlexeyProkhorov/qBoSlider/wiki/Widget-zone). So lets create a new slider for our store 😉.

* Open: Admin -> Admin menu -> Plugins -> qBoSlider -> Widget zones;
* Push '_Add widget zone_' button;
* Write widget zone name. This property is using just for widget zone searching. That's why you we can write anything that will help us to find widget zone faster;
* Write widget zone system name, for example '_home_page_bottom_'. We add autocomplete to help our users write standard nopCommerce widget zone names;
* Put other widget zone properties and push button '_Save and continue edit_';
* Great! Now you're create a new slider for widget zone🎉👍;
* Now add our new slides, which we add previously, to our widget zone slides collection;
* Param-pam-pam-pam 🎉😁.  Kudos! Now you've new [slider](https://github.com/iAlexeyProkhorov/qBoSlider/wiki/Widget-zone) with your own [slides](https://github.com/iAlexeyProkhorov/qBoSlider/wiki/Slide). Clear nopCommerce cache and go to page where widget zone locate (for example: '_home_page_bottom_' slider locates at bottom side of site home page);

## Few additional moments:
* qBoSlider supports so much widget zones and slides as you can create;
* One slide can be add to any widget zones. We can add slide to all store widget zones if it's really needed;
* We can create two or more widget zones with same system names. nopCommerce will display slider for first created widget zone;

