${
    using Typewriter.Extensions.WebApi;
	using System.Reflection;
	

	string test(Method c)
    {
		var attr= c.Attributes.FirstOrDefault(a=>a.Name=="WebApi");
		if(attr==null)
			return null;
        return  attr.Value.ToString().Split(',')[0];  //String.Join(",", c.Attributes.Select(a=>a.Name).ToArray());
    }
}
import { Injectable } from '@angular/core';
import { AXHttpService, HttpResult } from 'acorex-ui/acorex-ui';

	

@Injectable()$Classes(*Context)[
    export class $Name {
        constructor(private http:AXHttpService) {
        } $Methods[
        
        public $name($Parameters[$name: $Type][, ]):HttpResult<$Type>{
            return this.http.request({ url: `$test`, method: "$HttpMethod", params: $RequestData });
        }]
    }]
