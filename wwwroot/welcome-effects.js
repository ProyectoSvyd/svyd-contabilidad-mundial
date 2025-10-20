// Welcome Page - Floating Particles & Interactive Effects
// SVYD - Sistema de Contabilidad Mundial

class WelcomeEffects {
    constructor() {
        this.particles = [];
        this.particleCount = 50;
        this.isInitialized = false;
        this.animationFrame = null;
        
        this.init();
    }

    init() {
        if (this.isInitialized) return;
        
        // Esperar a que el DOM esté completamente cargado
        if (document.readyState === 'loading') {
            document.addEventListener('DOMContentLoaded', () => this.setupEffects());
        } else {
            this.setupEffects();
        }
        
        this.isInitialized = true;
    }

    setupEffects() {
        this.createFloatingParticles();
        this.setupFormInteractions();
        this.setupSkyEffects();
        this.setupResponsiveEffects();
        
        console.log('🌤️ Efectos de bienvenida SVYD activados');
    }

    createFloatingParticles() {
        const particlesContainer = document.querySelector('.floating-particles');
        if (!particlesContainer) return;

        // Limpiar partículas existentes
        particlesContainer.innerHTML = '';
        this.particles = [];

        for (let i = 0; i < this.particleCount; i++) {
            const particle = this.createParticle(i);
            particlesContainer.appendChild(particle);
            this.particles.push(particle);
        }
    }

    createParticle(index) {
        const particle = document.createElement('div');
        particle.className = 'particle';
        
        // Posiciones y delays aleatorios
        const x = Math.random() * 100;
        const y = Math.random() * 100;
        const delay = Math.random() * 15;
        const size = Math.random() * 3 + 2; // Entre 2-5px
        const opacity = Math.random() * 0.6 + 0.2; // Entre 0.2-0.8
        
        particle.style.setProperty('--x', `${x}%`);
        particle.style.setProperty('--y', `${y}%`);
        particle.style.setProperty('--delay', `${delay}s`);
        particle.style.width = `${size}px`;
        particle.style.height = `${size}px`;
        particle.style.opacity = opacity;
        
        // Variaciones de color para simular diferentes tipos de partículas
        const colors = [
            'rgba(255, 255, 255, 0.8)',
            'rgba(173, 216, 230, 0.6)', // Light blue
            'rgba(240, 248, 255, 0.7)', // Alice blue
            'rgba(230, 230, 250, 0.5)'  // Lavender
        ];
        const randomColor = colors[Math.floor(Math.random() * colors.length)];
        particle.style.background = randomColor;
        particle.style.boxShadow = `0 0 6px ${randomColor}`;
        
        return particle;
    }

    setupFormInteractions() {
        const inputs = document.querySelectorAll('.floating-input');
        const loginButton = document.querySelector('.signin-button');
        const altButtons = document.querySelectorAll('.alt-button');

        // Efectos especiales para inputs
        inputs.forEach(input => {
            input.addEventListener('focus', (e) => {
                this.createInputRipple(e.target);
                this.highlightFormCard();
            });

            input.addEventListener('blur', () => {
                this.removeFormCardHighlight();
            });

            // Efecto de escritura
            input.addEventListener('input', (e) => {
                this.createTypingEffect(e.target);
            });
        });

        // Efecto del botón principal
        if (loginButton) {
            loginButton.addEventListener('mouseenter', () => {
                this.createButtonGlow(loginButton);
            });

            loginButton.addEventListener('click', (e) => {
                this.createSuccessRipple(e.target);
            });
        }

        // Efectos para botones alternativos
        altButtons.forEach(button => {
            button.addEventListener('mouseenter', () => {
                this.createSoftGlow(button);
            });
        });
    }

    createInputRipple(input) {
        const ripple = document.createElement('div');
        ripple.className = 'input-ripple';
        ripple.style.cssText = `
            position: absolute;
            top: 50%;
            left: 50%;
            width: 0;
            height: 0;
            background: radial-gradient(circle, rgba(79, 172, 254, 0.3), transparent);
            border-radius: 50%;
            transform: translate(-50%, -50%);
            animation: inputRippleEffect 0.6s ease-out;
            pointer-events: none;
            z-index: 0;
        `;

        input.style.position = 'relative';
        input.parentNode.style.position = 'relative';
        input.parentNode.appendChild(ripple);

        // Crear keyframes dinámicamente
        if (!document.getElementById('input-ripple-keyframes')) {
            const style = document.createElement('style');
            style.id = 'input-ripple-keyframes';
            style.textContent = `
                @keyframes inputRippleEffect {
                    0% {
                        width: 0;
                        height: 0;
                        opacity: 1;
                    }
                    100% {
                        width: 200px;
                        height: 200px;
                        opacity: 0;
                    }
                }
            `;
            document.head.appendChild(style);
        }

        setTimeout(() => {
            if (ripple.parentNode) {
                ripple.parentNode.removeChild(ripple);
            }
        }, 600);
    }

    highlightFormCard() {
        const formContainer = document.querySelector('.login-form-container');
        if (formContainer) {
            formContainer.style.transform = 'translateY(-3px) scale(1.01)';
            formContainer.style.boxShadow = `
                0 30px 100px rgba(79, 172, 254, 0.15),
                0 15px 40px rgba(79, 172, 254, 0.1),
                inset 0 1px 0 rgba(255, 255, 255, 0.9)
            `;
        }
    }

    removeFormCardHighlight() {
        const formContainer = document.querySelector('.login-form-container');
        if (formContainer) {
            setTimeout(() => {
                if (!document.querySelector('.floating-input:focus')) {
                    formContainer.style.transform = '';
                    formContainer.style.boxShadow = '';
                }
            }, 100);
        }
    }

    createTypingEffect(input) {
        const sparkle = document.createElement('div');
        sparkle.innerHTML = '✨';
        sparkle.style.cssText = `
            position: absolute;
            top: -10px;
            right: 10px;
            font-size: 12px;
            animation: sparkleFloat 1s ease-out forwards;
            pointer-events: none;
            z-index: 100;
        `;

        input.parentNode.style.position = 'relative';
        input.parentNode.appendChild(sparkle);

        if (!document.getElementById('sparkle-keyframes')) {
            const style = document.createElement('style');
            style.id = 'sparkle-keyframes';
            style.textContent = `
                @keyframes sparkleFloat {
                    0% {
                        opacity: 1;
                        transform: translateY(0) scale(1);
                    }
                    100% {
                        opacity: 0;
                        transform: translateY(-20px) scale(0.5);
                    }
                }
            `;
            document.head.appendChild(style);
        }

        setTimeout(() => {
            if (sparkle.parentNode) {
                sparkle.parentNode.removeChild(sparkle);
            }
        }, 1000);
    }

    createButtonGlow(button) {
        button.style.boxShadow = `
            0 0 20px rgba(34, 197, 94, 0.4),
            0 0 40px rgba(34, 197, 94, 0.2),
            0 8px 25px rgba(34, 197, 94, 0.3)
        `;
    }

    createSuccessRipple(button) {
        const ripple = document.createElement('div');
        ripple.style.cssText = `
            position: absolute;
            top: 50%;
            left: 50%;
            width: 0;
            height: 0;
            background: radial-gradient(circle, rgba(34, 197, 94, 0.4), transparent);
            border-radius: 50%;
            transform: translate(-50%, -50%);
            animation: successRipple 0.8s ease-out;
            pointer-events: none;
        `;

        button.style.position = 'relative';
        button.appendChild(ripple);

        if (!document.getElementById('success-ripple-keyframes')) {
            const style = document.createElement('style');
            style.id = 'success-ripple-keyframes';
            style.textContent = `
                @keyframes successRipple {
                    0% {
                        width: 0;
                        height: 0;
                        opacity: 1;
                    }
                    100% {
                        width: 300px;
                        height: 300px;
                        opacity: 0;
                    }
                }
            `;
            document.head.appendChild(style);
        }

        setTimeout(() => {
            if (ripple.parentNode) {
                ripple.parentNode.removeChild(ripple);
            }
        }, 800);
    }

    createSoftGlow(button) {
        button.style.boxShadow = '0 6px 20px rgba(0, 0, 0, 0.15)';
    }

    setupSkyEffects() {
        const skyBackground = document.querySelector('.sky-background');
        if (!skyBackground) return;

        // Efecto de nubes moviéndose suavemente
        let cloudOffset = 0;
        
        const animateClouds = () => {
            cloudOffset += 0.1;
            const transform = `translateX(${Math.sin(cloudOffset) * 5}px) translateY(${Math.cos(cloudOffset * 0.7) * 3}px)`;
            skyBackground.style.transform = transform;
            
            this.animationFrame = requestAnimationFrame(animateClouds);
        };

        animateClouds();

        // Efectos de hover en paneles
        const panels = document.querySelectorAll('.info-card');
        panels.forEach((panel, index) => {
            panel.addEventListener('mouseenter', () => {
                panel.style.transform = `translateY(-5px) rotateY(${index % 2 === 0 ? '2deg' : '-2deg'})`;
                panel.style.background = 'rgba(255, 255, 255, 0.15)';
            });

            panel.addEventListener('mouseleave', () => {
                panel.style.transform = '';
                panel.style.background = '';
            });
        });
    }

    setupResponsiveEffects() {
        // Ajustar efectos según el tamaño de pantalla
        const updateParticleCount = () => {
            const width = window.innerWidth;
            if (width < 768) {
                this.particleCount = 25; // Menos partículas en móvil
            } else if (width < 1024) {
                this.particleCount = 35; // Cantidad media en tablet
            } else {
                this.particleCount = 50; // Cantidad completa en desktop
            }
            
            this.createFloatingParticles();
        };

        window.addEventListener('resize', debounce(updateParticleCount, 300));
        updateParticleCount(); // Ejecutar una vez al inicio
    }

    // Método para regenerar partículas si es necesario
    refreshParticles() {
        this.createFloatingParticles();
    }

    // Método para pausar animaciones (útil cuando la página no está visible)
    pauseAnimations() {
        if (this.animationFrame) {
            cancelAnimationFrame(this.animationFrame);
        }
        
        this.particles.forEach(particle => {
            particle.style.animationPlayState = 'paused';
        });
    }

    // Método para reanudar animaciones
    resumeAnimations() {
        this.particles.forEach(particle => {
            particle.style.animationPlayState = 'running';
        });
        
        this.setupSkyEffects(); // Reiniciar efectos de cielo
    }

    // Limpiar efectos al salir de la página
    destroy() {
        if (this.animationFrame) {
            cancelAnimationFrame(this.animationFrame);
        }
        
        // Remover todos los event listeners
        const elements = document.querySelectorAll('.floating-input, .signin-button, .alt-button, .info-card');
        elements.forEach(el => {
            el.replaceWith(el.cloneNode(true)); // Reemplazar para remover listeners
        });
        
        this.particles = [];
        this.isInitialized = false;
    }
}

// Utility function para debounce
function debounce(func, wait) {
    let timeout;
    return function executedFunction(...args) {
        const later = () => {
            clearTimeout(timeout);
            func(...args);
        };
        clearTimeout(timeout);
        timeout = setTimeout(later, wait);
    };
}

// Detectar cuando la página está visible/oculta para optimizar rendimiento
document.addEventListener('visibilitychange', () => {
    if (window.welcomeEffects) {
        if (document.hidden) {
            window.welcomeEffects.pauseAnimations();
        } else {
            window.welcomeEffects.resumeAnimations();
        }
    }
});

// Inicializar efectos automáticamente
document.addEventListener('DOMContentLoaded', () => {
    // Solo inicializar si estamos en la página de bienvenida
    if (document.querySelector('.welcome-container')) {
        window.welcomeEffects = new WelcomeEffects();
        
        // Añadir algunos efectos adicionales para mejorar la experiencia
        setTimeout(() => {
            const welcomeContainer = document.querySelector('.welcome-container');
            if (welcomeContainer) {
                welcomeContainer.style.opacity = '1';
                welcomeContainer.style.transform = 'scale(1)';
            }
        }, 100);
    }
});

// Limpiar al salir de la página
window.addEventListener('beforeunload', () => {
    if (window.welcomeEffects) {
        window.welcomeEffects.destroy();
    }
});

// Exportar para uso externo si es necesario
if (typeof module !== 'undefined' && module.exports) {
    module.exports = WelcomeEffects;
}