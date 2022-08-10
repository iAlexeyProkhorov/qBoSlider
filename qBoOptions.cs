//Copyright 2020 Alexey Prokhorov

//Licensed under the Apache License, Version 2.0 (the "License");
//you may not use this file except in compliance with the License.
//You may obtain a copy of the License at

//    http://www.apache.org/licenses/LICENSE-2.0

//Unless required by applicable law or agreed to in writing, software
//distributed under the License is distributed on an "AS IS" BASIS,
//WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//See the License for the specific language governing permissions and
//limitations under the License.

namespace Nop.Plugin.Widgets.qBoSlider
{
    /// <summary>
    /// Represents navigation elements displaying type
    /// </summary>
    public enum NavigationType : int
    {
        /// <summary>
        /// Never display navigation element
        /// </summary>
        None = 0,
        /// <summary>
        /// Display element on slider mouse drag event
        /// </summary>
        OnMouseDrag = 1,
        /// <summary>
        /// Always display navigation element
        /// </summary>
        Always = 2
    }

    public enum TransitionPlay : int
    {
        Random = 0,
        Sequence = 1
    }

    public enum DragOrientation : int
    {
        None = 0,
        Horizontal = 1,
        Vertical = 2, 
        Both = 3
    }

    /// <summary>
    /// Represents entity publication state variants
    /// </summary>
    public enum PublicationState : int
    {
        All = 0,
        Published = 5,
        Unpublished = 10
    }
}
