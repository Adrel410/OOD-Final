using System;
using System.Collections.Generic;
using System.Text;

namespace HellWorld {
    public class RegularLock : ILockable {

        private bool _isLocked;

        public RegularLock() {
            _isLocked = false;
        }

        public bool IsLocked {
            get { return _isLocked; }
        }

        public bool IsUnlocked {
            get { return !_isLocked; }
        }

        public bool Unlock() {
            bool result = true;
            _isLocked = false;
            return result;
        }
        public bool Lock() {
            bool result = true;
            _isLocked = true;
            return result;
        }

        public bool CanOpen {
            get {
                return _isLocked ? false : true;
            }
        }

        public bool CanClose {
            get {
                return true;
            }
        }
    }
}
