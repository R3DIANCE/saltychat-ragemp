// Copyright (c) 2019 saltmine.de - Timm Gasteier
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

using System;

namespace SaltyServer
{
    public static class Extension
    {
        public static bool TryGetSharedData<T>(this GTANetworkAPI.Entity entity, string key, out T value)
        {
            try
            {
                object valueHolder = entity.GetSharedData(key.ToString());

                if (valueHolder != null)
                {
                    if (typeof(T).IsEnum)
                    {
                        if (Enum.IsDefined(typeof(T), valueHolder))
                        {
                            value = (T)valueHolder;
                            return true;
                        }
                    }
                    else if (valueHolder is T)
                    {
                        value = (T)valueHolder;
                        return true;
                    }
                    else if (valueHolder.GetType() == typeof(String))
                    {
                        value = Newtonsoft.Json.JsonConvert.DeserializeObject<T>((string)valueHolder);
                        return true;
                    }
                }
            }
            catch
            {
                // do nothing
            }

            value = default;
            return false;
        }
    }
}
