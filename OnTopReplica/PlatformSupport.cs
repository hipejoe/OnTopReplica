﻿using System;
using System.Collections.Generic;
using System.Text;
using OnTopReplica.Platforms;
using System.Windows.Forms;

namespace OnTopReplica {
    abstract class PlatformSupport : IDisposable {

        public static PlatformSupport Create() {
            var os = Environment.OSVersion;
            
            if (os.Platform != PlatformID.Win32NT)
                return new Other();

            if (os.Version.Major == 6) {
                if (os.Version.Minor >= 1)
                    return new WindowsSeven();
                else
                    return new WindowsVista();
            }
            else {
                //Generic NT
                return new WindowsXp();
            }
        }

        /// <summary>
        /// Checks whether OnTopReplica is compatible with the platform.
        /// </summary>
        /// <returns>Returns false if OnTopReplica cannot run.</returns>
        public abstract bool CheckCompatibility();

        /// <summary>
        /// Initializes the application. Called once in the app lifetime.
        /// </summary>
        public virtual void InitApp() {
        }

        /// <summary>
        /// Gets the main OnTopReplica form.
        /// </summary>
        protected MainForm Form { get; private set; }

        /// <summary>
        /// Initializes a form.
        /// </summary>
        /// <param name="form">Form to initialize.</param>
        public virtual void InitForm(MainForm form) {
            Form = form;
        }

        public virtual void ShutdownApp() {
        }

        /// <summary>
        /// Hides a form in a way that it can be restored later by the user.
        /// </summary>
        /// <param name="form">Form to hide.</param>
        public virtual void HideForm(MainForm form) {
            form.Hide();
        }

        #region IDisposable Members

        bool _isDisposed = false;

        public void Dispose() {
            if (_isDisposed)
                return;

            this.ShutdownApp();
            _isDisposed = true;
        }

        #endregion
    }
}
