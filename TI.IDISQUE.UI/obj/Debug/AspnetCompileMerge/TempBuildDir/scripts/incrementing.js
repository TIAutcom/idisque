var click = document.querySelector("#click");
var reset = document.querySelector("#reset");
var result = document.querySelector("#result");
var counter = 0;

click.addEventListener('click',  () => {
  counter++;
  result.innerHTML = `${counter}`;
});

reset.addEventListener('click', () => {
  if (counter != 0) {
    counter--;
    result.innerHTML = `${counter}`;
  }
});
