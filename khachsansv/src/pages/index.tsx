import { useEffect, useState } from "react";
import Head from "next/head";
import Image from "next/image";
import Link from "next/link";
import styles from "../styles/Home.module.css";

export default function Home() {
  const [scrolled, setScrolled] = useState(false);

  useEffect(() => {
    const handleScroll = () => {
      setScrolled(window.scrollY > 10);
    };

    window.addEventListener("scroll", handleScroll);
    return () => {
      window.removeEventListener("scroll", handleScroll);
    };
  }, []);

  return (
    <>
      <Head>
        <title>Hotel Nhóm 1</title>
        <meta name="viewport" content="width=device-width, initial-scale=1.0" />
      </Head>

      {/* Navbar */}
      <nav className={`${styles.navbar} ${scrolled ? styles.scrolled : ""}`}>
        <div className={styles.container}>
          <Link href="/" className={styles.navbarBrand}>
            Hotel Nhóm 1
          </Link>
          <button
            className={styles.navbarToggler}
            type="button"
            data-bs-toggle="collapse"
            data-bs-target="#navbarNav"
          >
            <span className={styles.navbarTogglerIcon}></span>
          </button>
          <div className={styles.navbarCollapse} id="navbarNav">
            <ul className={styles.navbarNav}>
              <li className={styles.navItem}>
                <Link href="/" className={`${styles.navLink} ${styles.active}`}>
                  Trang chủ
                </Link>
              </li>
              <li className={styles.navItem}>
                <Link href="#" className={styles.navLink}>
                  Phòng
                </Link>
              </li>
              <li className={styles.navItem}>
                <Link href="#" className={styles.navLink}>
                  Đánh giá
                </Link>
              </li>
              <li className={styles.navItem}>
                <Link href="#" className={styles.navLink}>
                  Liên hệ
                </Link>
              </li>
              <li className={styles.navItem}>
                <Link href="#" className={styles.navLink}>
                  Tài khoản
                </Link>
              </li>
            </ul>
          </div>
        </div>
      </nav>

      {/* Hero Section */}
      <header className={styles.heroSection}>
        <div className={styles.heroOverlay}></div>
        <Image
          src="/hinhanhtrangchu/anhtbackgroundtrangchu.png"
          alt="Khách sạn sang trọng"
          layout="fill"
          objectFit="cover"
          priority
        />
        <div className={styles.heroContent}>
          <h1 className={styles.heroTitle}>Một trải nghiệm đáng nhớ</h1>
          <Link href="#" className={styles.btnPrimary}>
            Đặt chỗ ngay
          </Link>
        </div>
      </header>

      {/* Giới thiệu */}
      <section className={styles.introSection}>
        <div className={styles.container}>
          <div className={styles.row}>
            <div className={styles.colMd6}>
              <h2 className={styles.sectionTitle}>
                Về <span className={styles.textPurple}>Hotel Nhóm 1</span>
              </h2>
              <p>
                Chào mừng bạn đến với <span className={styles.textPurple}>Hotel Nhóm 1</span> - nơi
                mang đến trải nghiệm lưu trú sang trọng và thoải mái bậc nhất.
              </p>
              <p>
                Trải nghiệm không gian nghỉ dưỡng đẳng cấp với hệ thống phòng nghỉ sang trọng, nhà
                hàng ẩm thực đa dạng, khu spa thư giãn và các tiện ích cao cấp khác.
              </p>
              <Link href="#" className={styles.btnSecondary}>
                Xem thêm
              </Link>
            </div>
            <div className={styles.colMd6}>
              <Image
                src="/hinhanhtrangchu/anhabout.png"
                alt="Phòng khách sạn"
                width={600}
                height={400}
                className={styles.introImage}
              />
            </div>
          </div>
        </div>
      </section>

      {/* Phòng trưng bày */}
      <section className={styles.gallerySection}>
        <div className={styles.galleryOverlay}></div>
        <div className={styles.galleryContainer}>
          <h2 className={styles.galleryTitle}>Phòng trưng bày</h2>
          <div className={styles.galleryRow}>
            <div className={styles.galleryCol}>
              <Image
                src="/hinhanhtrangchu/anhphongtrungbay1.png"
                alt="Phòng 1"
                width={400}
                height={300}
                className={styles.galleryImg}
              />
            </div>
            <div className={styles.galleryCol}>
              <Image
                src="/hinhanhtrangchu/anhphongtrungbay2.png"
                alt="Phòng 2"
                width={400}
                height={300}
                className={styles.galleryImg}
              />
            </div>
            <div className={styles.galleryCol}>
              <Image
                src="/hinhanhtrangchu/anhphongtrungbay3.png"
                alt="Phòng 3"
                width={400}
                height={300}
                className={styles.galleryImg}
              />
            </div>
          </div>
          <Link href="#" className={styles.btnPrimary}>
            Khám phá ngay
          </Link>
        </div>
      </section>

      {/* Footer */}
      <footer className={styles.footer}>
        <div className={styles.container}>
          <div className={styles.footerRow}>
            <div className={styles.footerCol}>
              <h5 className={styles.footerTitle}>Hotel Nhóm 1</h5>
              <p>Sẽ mang lại cho bạn sự thoải mái mà bạn xứng đáng</p>
              <p>
                <strong>Địa chỉ:</strong> Nằm ngủ trong mơ
              </p>
              <p>
                <strong>Điện thoại:</strong> 0123456789
              </p>
              <p>
                <strong>Email:</strong> hotelnhom1@gmail.com
              </p>
            </div>
            <div className={styles.footerCol}>
              <h5>Dịch vụ</h5>
              <ul className={styles.footerList}>
                <li>
                  <Link href="#" className={styles.footerLink}>
                    Về chúng tôi
                  </Link>
                </li>
                <li>
                  <Link href="#" className={styles.footerLink}>
                    Phòng
                  </Link>
                </li>
                <li>
                  <Link href="#" className={styles.footerLink}>
                    Đánh giá
                  </Link>
                </li>
                <li>
                  <Link href="#" className={styles.footerLink}>
                    Liên hệ
                  </Link>
                </li>
              </ul>
            </div>
            <div className={styles.footerCol}>
              <h5>Theo dõi</h5>
              <ul className={styles.footerList}>
                <li>
                  <Link href="#" className={styles.footerLink}>
                    FACEBOOK
                  </Link>
                </li>
                <li>
                  <Link href="#" className={styles.footerLink}>
                    INSTAGRAM
                  </Link>
                </li>
                <li>
                  <Link href="#" className={styles.footerLink}>
                    TWITTER
                  </Link>
                </li>
                <li>
                  <Link href="#" className={styles.footerLink}>
                    SNAPCHAT
                  </Link>
                </li>
              </ul>
            </div>
          </div>
        </div>
      </footer>
    </>
  );
}