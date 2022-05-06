function dotNetStaticMethodTest() {
    DotNet.invokeMethodAsync("BlazorApp.Client", "GetCurrentCount")
        .then(result => {
            console.log(`Current count from Js is ${result}`);
        });
}

function dotNetInstanceMethodTest(dotNetHelper) {
    dotNetHelper.invokeMethodAsync("IncrementCount");
}

function getSelectedValues(sel) {
	var results = [];
	var i;
	for (i = 0; i < sel.options.length; i++) {
		if (sel.options[i].selected) {
			results[results.length] = sel.options[i].value;
		}
	}
	return results;
};