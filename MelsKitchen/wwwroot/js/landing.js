const aboutMe = document.querySelector("#about-me");
const popDishes = document.querySelector("#pop-dishes");
const contactMe = document.querySelector("#contact-me");

aboutMe.addEventListener("click", (e) => {
    e.preventDefault();
    const me = document.querySelector(".me").offsetTop;
    window.scrollTo({
        top: me - 54, //add your necessary value
    });
})

popDishes.addEventListener("click", (e) => {
    e.preventDefault();
    const dishes = document.querySelector(".dishes").offsetTop;
    window.scrollTo({
        top: dishes - 54, //add your necessary value
    });
})

contactMe.addEventListener("click", (e) => {
    e.preventDefault();
    const contact = document.querySelector(".contact").offsetTop;
    window.scrollTo({
        top: contact - 54, //add your necessary value
    });
})

//scroll to top button
const scrollToTop = document.querySelector(".scroll-to-top");

function buttonVisability() {
    if (document.documentElement.scrollTop <= 150) {
        scrollToTop.style.display = "none";
    } else {
        scrollToTop.style.display = "block";
    }
}

buttonVisability();

document.addEventListener("scroll", () => {
    buttonVisability();
})

scrollToTop.addEventListener("click", (e) => {
    e.preventDefault();
    document.documentElement.scrollTop = 0; 
})